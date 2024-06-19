using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCTSMA.PORTAL.SERVICES
{
    public class LoginRedirect: ComponentBase
    {
        [Inject] NavigationManager? NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            //var returnUrl = NavigationManager?.ToBaseRelativePath(NavigationManager.Uri);

            NavigationManager?.NavigateTo("/login", forceLoad: true);
            //Redirect("~/login");

            await base.OnInitializedAsync();
        }
    }
}
