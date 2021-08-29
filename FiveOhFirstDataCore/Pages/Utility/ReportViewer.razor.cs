﻿using FiveOhFirstDataCore.Core.Account;
using FiveOhFirstDataCore.Core.Account.Detail;
using FiveOhFirstDataCore.Core.Data;
using FiveOhFirstDataCore.Core.Services;

using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveOhFirstDataCore.Pages.Utility
{
    public partial class ReportViewer
    {
        [Inject]
        public IReportService ReportService { get; set; }

        [CascadingParameter]
        public Trooper? CurrentUser { get; set; }

        protected TrooperReport Report { get; set; }

        private Selection ReportListing { get; set; } = Selection.Mine;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if(firstRender)
            {
                await InitPagination();
                StateHasChanged();
            }
        }

        private async Task InitPagination()
        {
            switch (ReportListing)
            {
                case Selection.Viewable:
                    await base.InitalizeAsync(ReportService.GetTrooperReportsAsync,
                        ReportService.GetTrooperReportCountsAsync,
                        new object[] { CurrentUser?.Slot ?? Slot.Archived }, 15);
                    break;
                case Selection.Particiapted:
                    await base.InitalizeAsync(ReportService.GetParticipatingReportsAsync,
                        ReportService.GetParticipatingReportCountsAsync,
                        new object[] { CurrentUser?.Id ?? 0 }, 15);
                    break;

                default:
                case Selection.Mine:
                    await base.InitalizeAsync(ReportService.GetPersonalReportsAsync,
                        ReportService.GetPersonalReportCountsAsync,
                        new object[] { CurrentUser?.Id ?? 0 }, 15);
                    break;
            }
        }

        private async Task SelectionChanged(ChangeEventArgs e)
        {
            ReportListing = Enum.Parse<Selection>((string?)e.Value ?? "");
            await InitPagination();
        }

        private enum Selection
        {
            [Description("My Reports")]
            Mine,
            [Description("Viewable Reports")]
            Viewable,
            [Description("Participated Reports")]
            Particiapted
        }
    }
}
