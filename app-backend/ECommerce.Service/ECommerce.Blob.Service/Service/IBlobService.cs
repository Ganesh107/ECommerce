using ECommerce.Blob.Service.Model;
using System.Text;

namespace ECommerce.Blob.Service.Service
{
    public interface IBlobService
    {
        bool AddBlob(List<DocumentItem> documentItem, StringBuilder traceLog);
    }
}
