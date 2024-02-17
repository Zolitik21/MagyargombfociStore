namespace MagyargombfociStore.Models.DTOs
{
    public class ButtonFootballDisplayModel
    {

        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Categories> Categories { get; set; }

        public string STerm { get; set; } = "";
        public int CategoryId { get; set; } = 0;

    }
}
