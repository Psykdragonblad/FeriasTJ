using FeriasTJ.Application.Service;
using FeriasTJ.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using Moq;
using FeriasTJ.Infra.Interface;

namespace FeriasTJ.Test
{
    public class ExcelServiceProcessarArquivo
    {      
       
        [Fact]
        public void CriarArquivoComCaminhoInvalido()
        {
            //arrange
            FileUploadModel model = new FileUploadModel();
            //model.File = new
            var content = "";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content)); 
            var formFile = new Mock<IFormFile>();
            var fileName = "fileName.txt";
            formFile.Setup(f => f.FileName).Returns(fileName); 
            formFile.Setup(f => f.Length).Returns(stream.Length);
            formFile.Setup(f => f.OpenReadStream()).Returns(stream); 
            formFile.Setup(f => f.ContentDisposition).Returns($"attachment; filename={fileName}"); 
            formFile.Setup(f => f.ContentType).Returns("text/plain"); 
            formFile.Setup(f => f.CopyTo(It.IsAny<Stream>())).Callback<Stream>(s => stream.CopyTo(s));
            model.File = formFile.Object;
            //act
            ExcelService service = new ExcelService(fileName);

            //assert
            Assert.Throws<IndexOutOfRangeException>(()=> service.ProcessarExcelEmFerias(model));
        }
    }
}