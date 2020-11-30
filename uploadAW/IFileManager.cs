using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace uploadAW
{
    public interface IFileManager
    {
        bool FileValidate(FileUpload FileUpload1);

        int Length(FileUpload FileUpload1);

        StreamReader ler(string path);

        bool possuiRegistro(StreamReader streamReader);

        string proximaLinha(StreamReader streamReader);

        void saveAsArchive(FileUpload FileUpload1, string nomeArquivo);

        void mensagem(Label label, string mensagem);

    }
}
