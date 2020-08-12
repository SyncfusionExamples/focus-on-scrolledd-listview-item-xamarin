using Syncfusion.DataSource.Extensions;
using Syncfusion.GridCommon.ScrollAxis;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewXamarin
{
    public class Behavior : Behavior<ContentPage>
    {
        #region Fields

        SfListView ListView;
        Button Button;
        VisualContainer Container;
        #endregion

        #region Overrides
        protected override void OnAttachedTo(ContentPage bindable)
        {
            ListView = bindable.FindByName<SfListView>("listView");
            Button = bindable.FindByName<Button>("scrollButton");
            Container = ListView.GetVisualContainer();
            Button.Clicked += Button_Clicked;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            Button.Clicked -= Button_Clicked;
            Button = null;
            ListView = null;
            Container = null;
            base.OnDetachingFrom(bindable);
        }
        #endregion

        #region Event
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var index = ListView.DataSource.DisplayItems.Count() - 1;
            var item = ListView.DataSource.DisplayItems[index];
            ListView.LayoutManager.ScrollToRowIndex(index, Syncfusion.ListView.XForms.ScrollToPosition.End, true);

            await Task.Delay(1000);
            for (int i = 0; i < Container.Children.Count; i++)
            {
                if (Container.Children[i].BindingContext == item)
                {
                    var entry = (Container.Children[i] as ListViewItem).Content as Entry;
                    entry.Focus();
                }
            }
        }
        #endregion
    }
}