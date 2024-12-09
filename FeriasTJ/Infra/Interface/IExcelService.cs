namespace FeriasTJ.Infra.Interface
{
    public interface IExcelService
    {
        public void SaveData(List<string[]> data);

        public List<string[]> ReadData();
    }
}
