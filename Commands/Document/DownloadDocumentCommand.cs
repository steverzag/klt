using FloraYFaunaAPI.Validations;
using System;

namespace FloraYFaunaAPI.Commands.Document
{
    public class DownloadDocumentCommand
    {
        [ValidationGuid]
        public Guid Id { get; set; }
    }
}
