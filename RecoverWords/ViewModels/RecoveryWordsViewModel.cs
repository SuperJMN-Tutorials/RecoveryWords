using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Bogus;
using DynamicData;
using DynamicData.Binding;

namespace RecoverWords.ViewModels;

public class RecoveryWordsViewModel : IDisposable
{
    private readonly CompositeDisposable disposables = new();

    public RecoveryWordsViewModel(string[] recoveryWords)
    {
        var randomizedWords = new Faker().Random.Shuffle(recoveryWords).ToList();
        ISubject<int> selectedCountChanged = new Subject<int>();
        var words = randomizedWords.Select(word => new WordViewModel(word, selectedCountChanged)).ToList();

        Pallette = words;

        var changeStream = words
            .ToObservable()
            .ToObservableChangeSet()
            .DisposeMany()
            .AutoRefresh(x => x.IsSelected);

        var selectedLists = changeStream.Filter(x => x.IsSelected);

        selectedLists
            .Count()
            .Subscribe(selectedCountChanged)
            .DisposeWith(disposables);

        selectedLists
            .Sort(SortExpressionComparer<WordViewModel>.Ascending(x => x.SortIndex))
            .Bind(out var wordCollection)
            .Subscribe()
            .DisposeWith(disposables);

        Words = wordCollection;
    }

    public List<WordViewModel> Pallette { get; }

    public IEnumerable<WordViewModel> Words { get; }

    public void Dispose()
    {
        disposables.Dispose();
    }
}