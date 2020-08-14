# How to scroll and focus on ListView item in Xamarin.Forms (SfListView)

You can scroll to the particular [ListViewItem](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.ListViewItem.html) and set focus for the [Entry](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.entry) control loaded inside the [SfListView.ItemTemplate](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~ItemTemplate.html) in Xamarin.Forms [SfListView](https://help.syncfusion.com/xamarin/listview/overview).

You can also refer the following article.

https://www.syncfusion.com/kb/11882/how-to-scroll-and-focus-on-listview-item-in-xamarin-forms-sflistview

**XAML**

Setting [Behavior](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/behaviors/) for the [ContentPage](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.contentpage?view=xamarin-forms) and add [Button](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/button) to scroll to the particular item.

``` xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:ListViewXamarin"
             xmlns:syncfusion="clr-namespace:Syncfusion.ListView.XForms;assembly=Syncfusion.SfListView.XForms"
             x:Class="ListViewXamarin.MainPage">
    <ContentPage.BindingContext>
        <local:ContactsViewModel/>
    </ContentPage.BindingContext>
    <ContentPage.Behaviors>
        <local:Behavior/>
    </ContentPage.Behaviors>
  <ContentPage.Content>
        <Grid RowSpacing="0">
            <Grid.RowDefinitions>
                <RowDefinition Height="50"/>
                <RowDefinition Height="*"/>
            </Grid.RowDefinitions>
            <Button x:Name="scrollButton" Text="Scroll to the last item"/>
            <Grid x:Name="mainGrid" Grid.Row="1" Padding="10,25,10,10">
                <syncfusion:SfListView ItemsSource="{Binding ContactsInfo}" x:Name="listView">
                    <syncfusion:SfListView.ItemTemplate>
                        <DataTemplate>
                            <Entry x:Name="entry" BackgroundColor="DarkBlue" PlaceholderColor="LightBlue" Placeholder="{Binding ContactName}"/>
                        </DataTemplate>
                    </syncfusion:SfListView.ItemTemplate>
                </syncfusion:SfListView>
            </Grid>
        </Grid>
    </ContentPage.Content>
</ContentPage>
```
**C#**

In [Button.Clicked](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.button.clicked) event, handled scrolling of particular index using [ScrollToRowIndex](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.LayoutBase~ScrollToRowIndex.html) method. You can get the [VisualContainer](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.VisualContainer.html) using [GetVisualContainer](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.Control.Helpers.SfListViewHelper~GetVisualContainer.html) method and get the element loaded inside **ListViewItem** to set [Focus](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.visualelement.focus?view=xamarin-forms#Xamarin_Forms_VisualElement_Focus) for the **Entry** control.

``` c#
using Syncfusion.ListView.XForms.Control.Helpers;
namespace ListViewXamarin
{
    public class Behavior : Behavior<ContentPage>
    {
        SfListView ListView;
        Button Button;
        VisualContainer Container;
 
        protected override void OnAttachedTo(ContentPage bindable)
        {
            ListView = bindable.FindByName<SfListView>("listView");
            Button = bindable.FindByName<Button>("scrollButton");
            Container = ListView.GetVisualContainer();
            Button.Clicked += Button_Clicked;
 
            base.OnAttachedTo(bindable);
        }
 
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var index = ListView.DataSource.DisplayItems.Count() - 1;
            var item = ListView.DataSource.DisplayItems[index];
            ListView.LayoutManager.ScrollToRowIndex(index, Syncfusion.ListView.XForms.ScrollToPosition.End, false);
 
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
    }
}
```
**Output**

![ScrollAndFocus](https://github.com/SyncfusionExamples/focus-on-scrolledd-listview-item-xamarin/blob/master/ScreenShot/ScrollAndFocus.gif)
