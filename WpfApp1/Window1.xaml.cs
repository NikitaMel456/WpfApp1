using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {


        public Window1()
        {

            InitializeComponent();
            WpfApp1.Food_Orders_DBDataSet food_Orders_DBDataSet = ((WpfApp1.Food_Orders_DBDataSet)(this.FindResource("food_Orders_DBDataSet")));
            WpfApp1.Food_Orders_DBDataSetTableAdapters.ElemFromGroupsTableAdapter food_Orders_DBDataSetElemFromGroupsTableAdapter = new WpfApp1.Food_Orders_DBDataSetTableAdapters.ElemFromGroupsTableAdapter();
            System.Windows.Data.CollectionViewSource elemFromGroupsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("elemFromGroupsViewSource")));
            elemFromGroupsViewSource.View.MoveCurrentToFirst();
            food_Orders_DBDataSetElemFromGroupsTableAdapter.GetData(наименование_группыComboBox.Text);
            food_Orders_DBDataSetElemFromGroupsTableAdapter.Fill(food_Orders_DBDataSet.ElemFromGroups, наименование_группыComboBox.Text);
            elemFromGroupsViewSource.View.MoveCurrentToFirst();


            string s = DateTime.Now.ToString("dd.MM.yyyy");
            дата_заказаTextBox.Text = s;

            ZakazInMenu.IsEnabled = false;


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WpfApp1.Food_Orders_DBDataSet food_Orders_DBDataSet = ((WpfApp1.Food_Orders_DBDataSet)(this.FindResource("food_Orders_DBDataSet")));
            // Загрузить данные в таблицу Группы. Можно изменить этот код как требуется.
            WpfApp1.Food_Orders_DBDataSetTableAdapters.ГруппыTableAdapter food_Orders_DBDataSetГруппыTableAdapter = new WpfApp1.Food_Orders_DBDataSetTableAdapters.ГруппыTableAdapter();
            food_Orders_DBDataSetГруппыTableAdapter.Fill(food_Orders_DBDataSet.Группы);
            System.Windows.Data.CollectionViewSource группыViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("группыViewSource")));
            группыViewSource.View.MoveCurrentToFirst();
            // TODO: Добавить сюда код, чтобы загрузить данные в таблицу ElemFromGroups.
            // Не удалось создать этот код, поскольку метод food_Orders_DBDataSetElemFromGroupsTableAdapter.Fill отсутствует или имеет неизвестные параметры.

            ElemFromGroup();
            // TODO: Добавить сюда код, чтобы загрузить данные в таблицу Ingredients.
            // Не удалось создать этот код, поскольку метод food_Orders_DBDataSetIngredientsTableAdapter.Fill отсутствует или имеет неизвестные параметры.
            WpfApp1.Food_Orders_DBDataSetTableAdapters.IngredientsTableAdapter food_Orders_DBDataSetIngredientsTableAdapter = new WpfApp1.Food_Orders_DBDataSetTableAdapters.IngredientsTableAdapter();
            System.Windows.Data.CollectionViewSource ingredientsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ingredientsViewSource")));
            ingredientsViewSource.View.MoveCurrentToFirst();


        }

        private void elemFromGroupsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ElemFromGroup()
        {
            WpfApp1.Food_Orders_DBDataSet food_Orders_DBDataSet = ((WpfApp1.Food_Orders_DBDataSet)(this.FindResource("food_Orders_DBDataSet")));
            WpfApp1.Food_Orders_DBDataSetTableAdapters.ElemFromGroupsTableAdapter food_Orders_DBDataSetElemFromGroupsTableAdapter = new WpfApp1.Food_Orders_DBDataSetTableAdapters.ElemFromGroupsTableAdapter();
            System.Windows.Data.CollectionViewSource elemFromGroupsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("elemFromGroupsViewSource")));
            elemFromGroupsViewSource.View.MoveCurrentToFirst();
            food_Orders_DBDataSetElemFromGroupsTableAdapter.GetData(наименование_группыComboBox.Text);
            food_Orders_DBDataSetElemFromGroupsTableAdapter.Fill(food_Orders_DBDataSet.ElemFromGroups, наименование_группыComboBox.Text);
            //  food_Orders_DBDataSetElemFromGroupsTableAdapter.FillBy(food_Orders_DBDataSet.ElemFromGroups, наименование_группыComboBox.Text);
            elemFromGroupsViewSource.View.MoveCurrentToFirst();

        }

        private void showIngredients()
        {
            DataGridCell cell = GetCell(elemFromGroupsDataGrid.SelectedIndex, 0, elemFromGroupsDataGrid);
            TextBlock tb1 = cell.Content as TextBlock;
            наименованиеTextBox.Text = tb1.Text;
            WpfApp1.Food_Orders_DBDataSet food_Orders_DBDataSet = ((WpfApp1.Food_Orders_DBDataSet)(this.FindResource("food_Orders_DBDataSet")));
            WpfApp1.Food_Orders_DBDataSetTableAdapters.IngredientsTableAdapter food_Orders_DBDataSetIngredientsTableAdapter = new WpfApp1.Food_Orders_DBDataSetTableAdapters.IngredientsTableAdapter();
            System.Windows.Data.CollectionViewSource ingredientsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ingredientsViewSource")));
            food_Orders_DBDataSetIngredientsTableAdapter.GetData(tb1.Text);
            food_Orders_DBDataSetIngredientsTableAdapter.Fill(food_Orders_DBDataSet.Ingredients, tb1.Text);
            ingredientsViewSource.View.MoveCurrentToFirst();

            //WpfApp1.Food_Orders_DBDataSet food_Orders_DBDataSet = ((WpfApp1.Food_Orders_DBDataSet)(this.FindResource("food_Orders_DBDataSet")));
            WpfApp1.Food_Orders_DBDataSetTableAdapters.QueriesTableAdapter food_Orders_DBDataSetCountTableAdapter = new WpfApp1.Food_Orders_DBDataSetTableAdapters.QueriesTableAdapter();


            суммаTextBox.Text = food_Orders_DBDataSetCountTableAdapter.CountSummF(Convert.ToInt32(KolichTBX.Text), наименованиеTextBox.Text).ToString();
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            showIngredients();
        }

        private void ingredientsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            try
            {
                T child = default(T);
                int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < numVisuals; i++)
                {
                    Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                    child = v as T;
                    if (child == null)
                    {
                        child = GetVisualChild<T>(v);
                    }
                    if (child != null)
                    {
                        break;
                    }
                }
                return child;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ошибка получения данных из таблицы функция (GetVisualChild<T>)!\n\n" + exc.Message, "Ошибка выполнения приложения", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

        }

        public DataGridRow GetRow(int index, DataGrid dg)
        {
            try
            {
                DataGridRow row = (DataGridRow)dg.ItemContainerGenerator.ContainerFromIndex(index);
                if (row == null)
                {
                    dg.UpdateLayout();
                    dg.ScrollIntoView(dg.Items[index]);
                    row = (DataGridRow)dg.ItemContainerGenerator.ContainerFromIndex(index);

                }
                return row;
            }
            catch (Exception exc)
            {
               // MessageBox.Show("Ошибка получения данных из таблицы функция (GetRow)!\n\n" + exc.Message, "Ошибка выполнения приложения", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public DataGridCell GetCell(int row, int column, DataGrid dg)
        {
            try
            {
                DataGridRow rowContainer = GetRow(row, dg);

                if (rowContainer != null)
                {
                    System.Windows.Controls.Primitives.DataGridCellsPresenter presenter = GetVisualChild<System.Windows.Controls.Primitives.DataGridCellsPresenter>(rowContainer);

                    DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                    if (cell == null)
                    {
                        dg.ScrollIntoView(rowContainer, dg.Columns[column]);
                        cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                    }
                    return cell;
                }
                return null;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ошибка получения данных из таблицы функция (GetCell)!\n\n" + exc.Message, "Ошибка выполнения приложения", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private void наименование_группыComboBox_MouseMove(object sender, MouseEventArgs e)
        {
            ElemFromGroup();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (фИОTextBox.Text!=""||улицаTextBox.Text!="" || городTextBox.Text!=""||_Дом_квартираTextBox.Text!=""||номер_телефонаTextBox.Text!="") {
                WpfApp1.Food_Orders_DBDataSet food_Orders_DBDataSet = ((WpfApp1.Food_Orders_DBDataSet)(this.FindResource("food_Orders_DBDataSet")));
                WpfApp1.Food_Orders_DBDataSetTableAdapters.QueriesTableAdapter food_Orders_DBDataSetZakazchikTableAdapter = new WpfApp1.Food_Orders_DBDataSetTableAdapters.QueriesTableAdapter();
                food_Orders_DBDataSetZakazchikTableAdapter.InsertZakazchik1(фИОTextBox.Text, номер_телефонаTextBox.Text, городTextBox.Text, улицаTextBox.Text, _Дом_квартираTextBox.Text);





                WpfApp1.Food_Orders_DBDataSetTableAdapters.QueriesTableAdapter food_Orders_DBDataSetNumTableAdapter = new WpfApp1.Food_Orders_DBDataSetTableAdapters.QueriesTableAdapter();
                Random rnd = new Random();
                int vod = rnd.Next(1, (int)food_Orders_DBDataSetNumTableAdapter.CountKurierF());
                food_Orders_DBDataSetNumTableAdapter.InsertZakazy1(DateTime.Now, vod, фИОTextBox.Text, номер_телефонаTextBox.Text);
                ZakazInMenu.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Null");
            }
        }

        private void DataCheck_Checked(object sender, RoutedEventArgs e)
        {
            AcceptBut.IsEnabled = true;
        }

        private void DataCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            AcceptBut.IsEnabled = false;
        }

        private void номер_телефонаTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void суммаTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void наименованиеTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WpfApp1.Food_Orders_DBDataSetTableAdapters.QueriesTableAdapter food_Orders_DBDataSetNumTableAdapter = new WpfApp1.Food_Orders_DBDataSetTableAdapters.QueriesTableAdapter();
            food_Orders_DBDataSetNumTableAdapter.ZakazIzMenu(фИОTextBox.Text, номер_телефонаTextBox.Text, наименованиеTextBox.Text, Convert.ToInt32(KolichTBX.Text), DateTime.Now);
            ZakazInMenu.IsEnabled = true;

           
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // TODO: Добавить сюда код, чтобы загрузить данные в таблицу ShowZakaz.
            // Не удалось создать этот код, поскольку метод food_Orders_DBDataSetShowZakazTableAdapter.Fill отсутствует или имеет неизвестные параметры.
            try{
                WpfApp1.Food_Orders_DBDataSet food_Orders_DBDataSet = ((WpfApp1.Food_Orders_DBDataSet)(this.FindResource("food_Orders_DBDataSet")));
                WpfApp1.Food_Orders_DBDataSetTableAdapters.ShowZakazTableAdapter food_Orders_DBDataSetShowZakazTableAdapter = new WpfApp1.Food_Orders_DBDataSetTableAdapters.ShowZakazTableAdapter();
                System.Windows.Data.CollectionViewSource showZakazViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("showZakazViewSource")));
                showZakazViewSource.View.MoveCurrentToFirst();
                food_Orders_DBDataSetShowZakazTableAdapter.GetData(фИОTextBox.Text, номер_телефонаTextBox.Text);
                food_Orders_DBDataSetShowZakazTableAdapter.Fill(food_Orders_DBDataSet.ShowZakaz, фИОTextBox.Text, номер_телефонаTextBox.Text);
                showZakazViewSource.View.MoveCurrentToFirst();

                WpfApp1.Food_Orders_DBDataSetTableAdapters.QueriesTableAdapter food_Orders_DBDataSetNumTableAdapter = new WpfApp1.Food_Orders_DBDataSetTableAdapters.QueriesTableAdapter();
                ResSummTBX.Text = food_Orders_DBDataSetNumTableAdapter.CommPrice(фИОTextBox.Text, номер_телефонаTextBox.Text).ToString();
            }
            catch (Exception)
            {

            }

        }

        private void showZakazDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //try
            //{
            if (GetCell(showZakazDataGrid.SelectedIndex, 0, showZakazDataGrid) != null )
            {
                if ( GetCell(showZakazDataGrid.SelectedIndex, 1, showZakazDataGrid) != null) {
                    DataGridCell cell = GetCell(showZakazDataGrid.SelectedIndex, 0, showZakazDataGrid);
                    TextBlock tb1 = cell.Content as TextBlock;
                    NaimDel.Text = tb1.Text;
                    DataGridCell cell1 = GetCell(showZakazDataGrid.SelectedIndex, 1, showZakazDataGrid);
                    TextBlock tb11 = cell1.Content as TextBlock;
                    KolDel.Text = tb11.Text;
                }
                else
                {
                    //WpfApp1.Food_Orders_DBDataSet food_Orders_DBDataSet = ((WpfApp1.Food_Orders_DBDataSet)(this.FindResource("food_Orders_DBDataSet")));
                    //WpfApp1.Food_Orders_DBDataSetTableAdapters.ShowZakazTableAdapter food_Orders_DBDataSetShowZakazTableAdapter = new WpfApp1.Food_Orders_DBDataSetTableAdapters.ShowZakazTableAdapter();
                    //System.Windows.Data.CollectionViewSource showZakazViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("showZakazViewSource")));
                    //food_Orders_DBDataSetShowZakazTableAdapter.GetData(фИОTextBox.Text, номер_телефонаTextBox.Text);
                    //food_Orders_DBDataSetShowZakazTableAdapter.Fill(food_Orders_DBDataSet.ShowZakaz, фИОTextBox.Text, номер_телефонаTextBox.Text);
                    //showZakazViewSource.View.MoveCurrentToFirst();
                }
            }
            else
            {
                //WpfApp1.Food_Orders_DBDataSet food_Orders_DBDataSet = ((WpfApp1.Food_Orders_DBDataSet)(this.FindResource("food_Orders_DBDataSet")));
                //WpfApp1.Food_Orders_DBDataSetTableAdapters.ShowZakazTableAdapter food_Orders_DBDataSetShowZakazTableAdapter = new WpfApp1.Food_Orders_DBDataSetTableAdapters.ShowZakazTableAdapter();
                //System.Windows.Data.CollectionViewSource showZakazViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("showZakazViewSource")));
                //food_Orders_DBDataSetShowZakazTableAdapter.GetData(фИОTextBox.Text, номер_телефонаTextBox.Text);
                //food_Orders_DBDataSetShowZakazTableAdapter.Fill(food_Orders_DBDataSet.ShowZakaz, фИОTextBox.Text, номер_телефонаTextBox.Text);
                //showZakazViewSource.View.MoveCurrentToFirst();
            }
            //}
            //catch(Exception)
            //{

            //}
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            WpfApp1.Food_Orders_DBDataSetTableAdapters.QueriesTableAdapter food_Orders_DBDataSetNumTableAdapter = new WpfApp1.Food_Orders_DBDataSetTableAdapters.QueriesTableAdapter();
            food_Orders_DBDataSetNumTableAdapter.DeleteZakazIzMenu(фИОTextBox.Text, номер_телефонаTextBox.Text, NaimDel.Text, DateTime.Now);

            NaimDel.Text = "";
            KolDel.Text = "";
         
        }
    }
}
