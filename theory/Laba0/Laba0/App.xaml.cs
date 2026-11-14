using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Laba0
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindow mw;
        SQLConnection conn = new SQLConnection("Data Source=192.168.221.12;Initial Catalog=Skoraia; USER ID=user01;Password=01");
        public void App_Startup(object sender, EventArgs e)
        {
            //this.PerformAsyncTasks();
            this.mw = new MainWindow(this);
            this.mw.Show();
            this.mw.ClearCatalogList();
            this.conn.Init();
            MessageBox.Show(this.conn.GetString("select * from Raschet1").First<string>());
            //IEnumerable<CatalogCard> a = this.conn.GetProduct("select Products.id, Products.name, Description.value, Products.cal, Products.img, Products.price, Products.stock from Products inner join Description on Products.desctiptionid = Description.productId");
            //CatalogCard[] cards = this.conn.GetProduct("select Products.id, Products.name, Description.value, Products.cal, Products.img, Products.price, Products.stock from Products inner join Description on Products.desctiptionid = Description.productId").ToArray();
            //this.mw.UpdateCatalogCards(cards);
        }
        //public async Task PerformAsyncTasks()
        //{
        //    await Task.Run(() => this.conn.Init()); //разобраться с интерфейсами и асинком тут
        //    CatalogCard[] cards = this.conn.GetProduct("select Products.id, Products.name, Description.value, Products.cal, Products.img, Products.price, Products.stock inner join Description on Products.desctiptionid = Description.productId").ToArray();
        //    this.mw.UpdateCatalogCards(cards);
        //}
    }
}
