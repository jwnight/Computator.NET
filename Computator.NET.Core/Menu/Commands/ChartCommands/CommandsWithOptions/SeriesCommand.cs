using Computator.NET.Core.Helpers;
using Computator.NET.Core.Menu.Commands.DummyCommands;
using Computator.NET.Core.Model;
using Computator.NET.DataTypes;

namespace Computator.NET.Core.Menu.Commands.ChartCommands.CommandsWithOptions
{
    //TODO: make it work somehow with good API from Chart2D

    internal class SeriesCommand : DummyCommand
    {
        private ISharedViewState _sharedViewState;
        /*private class SeriesOption : ChartOption
 {

     public override void Execute()
     {
         throw new System.NotImplementedException();
     }

     public SeriesOption(IDictionary<CalculationsMode, IChart> charts) : base(charts)
     {
         this.IsOption = true;
     }
 }*/

        public SeriesCommand(ISharedViewState sharedViewState) : base(MenuStrings.series_Text)
        {
            _sharedViewState = sharedViewState;
            BindingUtils.OnPropertyChanged(_sharedViewState, nameof(_sharedViewState.CalculationsMode),
                () => Visible = _sharedViewState.CalculationsMode == CalculationsMode.Real);
        }
    }
}