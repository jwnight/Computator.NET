using System;
using System.Collections.Generic;
using System.Linq;
using Accord.Collections;
using Computator.NET.Charting.ComplexCharting;
using Computator.NET.Core.Helpers;
using Computator.NET.Core.Menu.Commands.DummyCommands;
using Computator.NET.Core.Model;
using Computator.NET.DataTypes;
using Computator.NET.DataTypes.Charts;

namespace Computator.NET.Core.Menu.Commands.ChartCommands.CommandsWithOptions
{
    public class ColorAssigmentCommand : DummyCommand
    {
        private readonly ISharedViewState _sharedViewState;
        public ColorAssigmentCommand(IDictionary<CalculationsMode, IChart> charts, ISharedViewState sharedViewState)
            : base(MenuStrings.colorAssignmentToolStripMenuItem_Text)
        {
            _sharedViewState = sharedViewState;
            Visible = _sharedViewState.CalculationsMode == CalculationsMode.Complex;
            BindingUtils.OnPropertyChanged(_sharedViewState, nameof(_sharedViewState.CalculationsMode),
                () => Visible = _sharedViewState.CalculationsMode == CalculationsMode.Complex);


            var list = new List<IToolbarCommand>();

            foreach (var colorAssigment in Enum.GetValues(typeof(AssignmentOfColorMethod))
                .Cast<AssignmentOfColorMethod>())
            {
                list.Add(new ColorAssigmentOption(charts, colorAssigment,_sharedViewState));
            }
            ChildrenCommands = list;
        }

        private class ColorAssigmentOption : ChartOption
        {
            private readonly AssignmentOfColorMethod assignmentOfColorMethod;

            public ColorAssigmentOption(IDictionary<CalculationsMode, IChart> charts,
                AssignmentOfColorMethod assignmentOfColorMethod, ISharedViewState sharedViewState) : base(assignmentOfColorMethod, charts,sharedViewState)
            {
                this.assignmentOfColorMethod = assignmentOfColorMethod;
                IsOption = true;
                Checked = complexChart.ColorAssignmentMethod == assignmentOfColorMethod;

                BindingUtils.OnPropertyChanged(complexChart, nameof(complexChart.ColorAssignmentMethod), () =>
                    Checked = complexChart.ColorAssignmentMethod == assignmentOfColorMethod);
            }

            public override void Execute()
            {
                complexChart.ColorAssignmentMethod = assignmentOfColorMethod;
                complexChart.Redraw();
            }
        }
    }
}