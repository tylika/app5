    using app5.Models;
    using System.Collections.ObjectModel;
namespace app5
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Item> Items { get; set; } = new();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            // Додаємо тестові товари
            Items.Add(new Product { Name = "Молоко", Price = 32.5m, CountryOfOrigin = "Україна", PackagingDate = DateTime.Now, ShelfLife = 7, Quantity = 1, Unit = "л" });
            Items.Add(new Book { Name = "C# Програмування", Price = 400m, CountryOfOrigin = "США", PackagingDate = DateTime.Now, Pages = 500, Publisher = "Навчальна книга", Authors = new List<string> { "Омельчук", "Русіна" } });
        }
        private async void OnAddItemClicked(object sender, EventArgs e)
        {
            // Запитуємо тип товару
            string itemType = await DisplayActionSheet("Виберіть тип елемента", "Скасувати", null, "Продукт", "Книга");

            if (itemType == "Продукт")
            {
                // Запитуємо дані для товару (Product)
                string name = await DisplayPromptAsync("Додати продукт", "Введіть назву продукту:");
                string priceInput = await DisplayPromptAsync("Додати продукт", "Введіть ціну:");
                decimal price = decimal.TryParse(priceInput, out decimal result) ? result : 0;

                string country = await DisplayPromptAsync("Додати продукт", "Введіть країну походження:");
                string quantityInput = await DisplayPromptAsync("Додати продукт", "Введіть кількість:");
                int quantity = int.TryParse(quantityInput, out int qty) ? qty : 1;

                string unit = await DisplayPromptAsync("Додати продукт", "Введіть одиницю (наприклад, кг, л):");
                string shelfLifeInput = await DisplayPromptAsync("Додати продукт", "Введіть термін зберігання (днів):");
                int shelfLife = int.TryParse(shelfLifeInput, out int sl) ? sl : 0;

                Items.Add(new Product
                {
                    Name = name,
                    Price = price,
                    CountryOfOrigin = country,
                    PackagingDate = DateTime.Now,
                    ShelfLife = shelfLife,
                    Quantity = quantity,
                    Unit = unit
                });
            }
            else if (itemType == "Книга")
            {
                // Запитуємо дані для книги (Book)
                string name = await DisplayPromptAsync("Додати книгу", "Введіть назву книги:");
                string priceInput = await DisplayPromptAsync("Додати книгу", "Введіть ціну:");
                decimal price = decimal.TryParse(priceInput, out decimal result) ? result : 0;

                string country = await DisplayPromptAsync("Додати книгу", "Введіть країну походження:");
                string publisher = await DisplayPromptAsync("Додати книгу", "Введіть видавця:");

                string pagesInput = await DisplayPromptAsync("Додати книгу", "Введіть кількість сторінок:");
                int pages = int.TryParse(pagesInput, out int pg) ? pg : 0;

                string authorsInput = await DisplayPromptAsync("Додати книгу", "Введіть авторів (через кому):");
                List<string> authors = authorsInput.Split(',').Select(a => a.Trim()).ToList();

                Items.Add(new Book
                {
                    Name = name,
                    Price = price,
                    CountryOfOrigin = country,
                    PackagingDate = DateTime.Now,
                    Publisher = publisher,
                    Pages = pages,
                    Authors = authors
                });
            }
        }

        private void OnDeleteItemClicked(object sender, EventArgs e)
        {
            if (ItemList.SelectedItem is Item selectedItem)
            {
                Items.Remove(selectedItem);
            }
        }
    }

}
