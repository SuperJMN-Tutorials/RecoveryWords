using System;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace RecoverWords.ViewModels;

public class WordViewModel : ViewModelBase, IDisposable
{
    private readonly ObservableAsPropertyHelper<int> currentSortIndex;

    public WordViewModel(string word, IObservable<int> selectedCountChanged)
    {
        Word = word;
        currentSortIndex = selectedCountChanged.ToProperty(this, x => x.CurrentSortIndex);

        this.WhenAnyValue(x => x.IsSelected)
            .Select(_ => CurrentSortIndex)
            .BindTo(this, x => x.SortIndex);
    }

    public string Word { get; }

    private int CurrentSortIndex => currentSortIndex.Value;

    [Reactive] public bool IsSelected { get; set; }
    [Reactive] public int SortIndex { get; set; }

    public void Dispose()
    {
        currentSortIndex.Dispose();
    }
}