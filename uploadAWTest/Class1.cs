using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using uploadAW;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace uploadAWTest
{
    [TestFixture]
    public class WebForm1Test
    {
        //[Test]
        //public void LerTest()
        //{
        //    string nomeArquivo = "planilha_tatica";
        //    WebForm1 webForm1 = new WebForm1();

        //    var mock = new Mock<IFileManager>();
        //    mock.Setup(action => action.FileValidate(It.IsAny<FileUpload>())).Returns(true);
        //    mock.Setup(action => action.Length(It.IsAny<FileUpload>())).Returns(5884);

        //    webForm1.Ler(nomeArquivo);
        //}

        [Test]
        public void btnEnviarArquivo_ClickTest()
        {
            WebForm1 webForm1 = new WebForm1();

            var mock = new Mock<IFileManager>();
            mock.Setup(action => action.FileValidate(It.IsAny<FileUpload>())).Returns(true);
            mock.Setup(action => action.Length(It.IsAny<FileUpload>())).Returns(5884);
            mock.Setup(action => action.saveAsArchive(It.IsAny<FileUpload>(), It.IsAny<string>()));

            mock.Setup(action => action.ler("planilha_tatica")).Returns(It.IsAny<StreamReader>());
            mock.SetupSequence(action => action.possuiRegistro(It.IsAny<StreamReader>())).Returns(true).Returns(true).Returns(true).Returns(false);
            mock.SetupSequence(action => action.proximaLinha(It.IsAny<StreamReader>())).Returns("01234567891011;006183479;202010;01").Returns("0123456789101;006183479;202010;01").Returns("01234567891011;00618347;202011;01");

            webForm1.fileManager = mock.Object;

            webForm1.btnEnviarArquivo_Click(It.IsAny<object>(), It.IsAny<EventArgs>());


        }

        //    [Test]
        //public void getAnoMesVigenciaArquivoTest()
        //{
        //    WebForm1 webForm1 = new WebForm1();

        //    string[] linhaArquivo1 = new string[4];
        //    linhaArquivo1[0] = "01234567891011";
        //    linhaArquivo1[1] = "006183479";
        //    linhaArquivo1[2] = "202010";
        //    linhaArquivo1[3] = "01";

        //    string[] linhaArquivo2 = new string[4];
        //    linhaArquivo2[0] = "01234567891011";
        //    linhaArquivo2[1] = "006183479";
        //    linhaArquivo2[2] = "202010";
        //    linhaArquivo2[3] = "01";

        //    string[] linhaArquivo3 = new string[4];
        //    linhaArquivo3[0] = "01234567891011";
        //    linhaArquivo3[1] = "006183479";
        //    linhaArquivo3[2] = "202110";
        //    linhaArquivo3[3] = "01";

        //    string[] linhaArquivo4 = new string[4];
        //    linhaArquivo4[0] = "01234567891011";
        //    linhaArquivo4[1] = "006183479";
        //    linhaArquivo4[2] = "202011";
        //    linhaArquivo4[3] = "01";

        //    List<string[]> listRegistrosArquivo = new List<string[]>();
        //    listRegistrosArquivo.Add(linhaArquivo1);
        //    listRegistrosArquivo.Add(linhaArquivo2);
        //    listRegistrosArquivo.Add(linhaArquivo3);
        //    listRegistrosArquivo.Add(linhaArquivo4);


        //    HashSet<string> anoMesVigenciaUnicosResult = webForm1.getAnoMesVigenciaArquivo(listRegistrosArquivo);

        //    Assert.IsTrue(anoMesVigenciaUnicosResult.Count() == 3);
        //}
    }
}
