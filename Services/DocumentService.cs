//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ChurchApi.Data;
//using ChurchApi.DTOs;
//using ChurchApi.Helpers;
//using ChurchApi.Models;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ChurchApi.Services
//{


//    public class DocumentFilter
//    {
//        [FromQuery]
//        public string Text { get; set; }

//        public string RootPath { get; set; }

//        [FromQuery(Name = "_page")]
//        public int Page { get; set; }

//        [FromQuery(Name = "_size")]
//        public int Size { get; set; }

//        public int Skip() { return (Page - 1) * Size; }

//        public IQueryable<Document> BuildQuery(IQueryable<Document> query)
//        {
//            //if (!string.IsNullOrEmpty(Name)) query = query.Where(q => q.Name.Contains(Name));
//            return query;
//        }
//    }

//    public interface IDocumentService : IModelService<DocumentDto>
//    {
//        Task<List<DocumentCardDto>> GetDocuments(long referenceId, string username);
//        Task<List<DocumentCardDto>> GetProcessedDocuments(long id, string username);
//        Task<Tuple<MemoryStream, string>> GetFile(long id, string rootPath);
//        Task<Tuple<MemoryStream, string>> DownloadTemplate(string rootPath);
//        Task<string> UploadFile(IFormFile file, string rootPath);
//        Task<Tuple<ICollection<DocumentCardDto>, long>> Query(DocumentFilter filter, string username);
//        Task<DocumentCardDto> GetTemplate();
//        Task<bool> DeleteDocument(long id, string rootPath);
//        Task<StatCardDto> FetchStats();
//    }


//    public class DocumentService : BaseService<DocumentDto, Document>, IDocumentService
//    {
//        public DocumentService(AppDbContext context, IMapper mapper) : base(context, mapper)
//        {
//        }

//        public async Task<Tuple<MemoryStream, string>> GetFile(long id, string rootPath)
//        {
//            var document = await _context.Documents.FindAsync(id);
//            var folder = Path.Combine(rootPath, "App_Data", "Files");
//            var filePath = Path.Combine(folder, $"{document.FileName}");

//            var memory = new MemoryStream();
//            using (var stream = new FileStream(filePath, FileMode.Open))
//            {
//                await stream.CopyToAsync(memory);
//            }
//            memory.Position = 0;

//            var stat = await SaveStats(true);
//            return new Tuple<MemoryStream, string>(memory, document.FileName);
//        }

//        public async Task<Tuple<MemoryStream, string>> DownloadTemplate(string rootPath)
//        {
//            var document = await _context.Documents.FirstOrDefaultAsync(q => q.IsTemplate);
//            var folder = Path.Combine(rootPath, "App_Data", "Files");
//            var filePath = Path.Combine(folder, $"{document.FileName}");

//            var memory = new MemoryStream();
//            using (var stream = new FileStream(filePath, FileMode.Open))
//            {
//                await stream.CopyToAsync(memory);
//            }
//            memory.Position = 0;
//            return new Tuple<MemoryStream, string>(memory, document.FileName);
//        }

//        public async Task<string> UploadFile(IFormFile file, string rootPath)
//        {
//            //Todo: Validate file extension 
//            //const allowedExtension = ["pdf", "png", "jpg", "jpeg", "xls", "xlsx", "doc", "docx"];

//            var folder = Path.Combine(rootPath, "App_Data", "Files");
//            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
//            var fileName = $"{DateTime.Now:yyMMddhhmmss}-{GeneralHelpers.TokenCode(4)}-{file.FileName}";
//            var path = Path.Combine(folder, $"{fileName}");
//            using var stream = new FileStream(path, FileMode.Create);
//            await file.CopyToAsync(stream);

//            var stat = await SaveStats(false);

//            return fileName;
//        }

//        public override async Task<long> Save(DocumentDto record)
//        {
//            var user = _context.Users.FirstOrDefault(q => q.UserName == record.Username);
//            var document = new Document
//            {
//                ReferenceId = record.ReferenceId,
//                Description = record.Description,
//                FileName = await SaveFile(record.File, record.RootPath),
//                FileLabel = record.FileLabel,
//                RootPath = record.RootPath,
//                Timestamp = DateTime.UtcNow,
//                CreatedBy = record.Username,
//                ModifiedBy = record.Username,
//                IsTemplate = record.IsTemplate,
//                //BankId = user?.BankId,
//                DocumentStatus = DocumentStatus.Pending,
//            };

//            if (!record.IsTemplate)
//            {
//                var stat = await SaveStats(false);
//            }

//            if (document.ReferenceId != null)
//            {
//                var original = await _context.Documents.FindAsync(document.ReferenceId);
//                original.DocumentStatus = DocumentStatus.Complete;
//                _context.Documents.Update(original);
//                //_context.SaveChanges();
//            }
//            await _context.Documents.AddAsync(document);
//            _context.SaveChanges();





//            return document.Id;
//        }


//        public override async Task<long> Update(DocumentDto record)
//        {
//            var doc = await _context.Documents.FindAsync(record.Id);
//            if (doc != null)
//            {

//                doc.ReferenceId = record.ReferenceId;
//                doc.Description = record.Description;
//                doc.FileName = await UpdateFile(record, doc.FileName, record.RootPath);
//                doc.FileLabel = record.FileLabel;
//                doc.RootPath = record.RootPath;
//                doc.Timestamp = DateTime.UtcNow;
//                doc.ModifiedBy = record.Username;
//                doc.IsTemplate = record.IsTemplate;
//                //doc.BankId = record.BankId;


//                _context.Documents.Update(doc);
//                _context.SaveChanges();
//            }

//            return doc.Id;
//        }


//        private async Task<string> UpdateFile(DocumentDto record, string oldFileName, string rootPath)
//        {
//            var res = RemoveFile(oldFileName, rootPath);
//            var fileName = $"{DateTime.Now:yyMMddhhmmss}-{GeneralHelpers.TokenCode(4)}-{record.File.FileName}";
//            if (res)
//            {
//                var folder = Path.Combine(rootPath, "App_Data", "Files");
//                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
//                var path = Path.Combine(folder, $"{fileName}");
//                using var stream = new FileStream(path, FileMode.Create);
//                await record.File.CopyToAsync(stream);


//            }
//            return fileName;
//        }

//        public async Task<bool> DeleteDocument(long id, string rootPath)
//        {
//            var doc = await _context.Documents.FindAsync(id);
//            if (doc != null)
//            {
//                var res = RemoveFile(doc.FileName, rootPath);
//                if (res)
//                {
//                    _context.Documents.Remove(doc);
//                    _context.SaveChanges();
//                };
//            }

//            return true;
//        }

//        private bool RemoveFile(string fileName, string rootPath)
//        {
//            //var doc = await _context.Documents.FindAsync(id);
//            //_context.Documents.Remove(doc);
//            //_context.SaveChanges();
//            var folder = Path.Combine(rootPath, "App_Data", "Files");
//            var filePath = Path.Combine(folder, fileName);
//            if (File.Exists(filePath)) File.Delete(filePath);
//            return true;
//        }

//        public async Task<Tuple<ICollection<DocumentCardDto>, long>> Query(DocumentFilter filter, string username)
//        {
//            var user = _context.Users.FirstOrDefault(q => q.UserName == username);
//            //var data= new List<DocumentCardDto>();
//            if (user.View == View.Portal)
//            {
//                var query = filter.BuildQuery(_context.Documents.Where(x => x.Id > 0 && x.CreatedBy == username && !x.IsTemplate && x.ReferenceId == null));
//                var data = await query.OrderByDescending(x => x.Id)
//                 .Skip(filter.Skip()).Take(filter.Size)
//                 .Select(x => new DocumentCardDto
//                 {
//                     Id = x.Id,
//                     Name = x.FileLabel,
//                     Description = x.Description,
//                     Username = x.CreatedBy,
//                     Timestamp = x.CreatedAt,
//                     //Bank=x.Bank.Name,
//                     User = user.Name,
//                     Status = x.DocumentStatus
//                     //CanDelete = x.CreatedBy == username
//                 }).ToListAsync();

//                var total = await query.CountAsync();
//                return new Tuple<ICollection<DocumentCardDto>, long>(data, total);
//            }
//            else
//            {
//                var query = filter.BuildQuery(_context.Documents.Where(x => x.Id > 0 && !x.IsTemplate && x.ReferenceId == null));
//                var data = await query.OrderByDescending(x => x.Id)
//                   .Skip(filter.Skip()).Take(filter.Size)
//                   .Select(x => new DocumentCardDto
//                   {
//                       Id = x.Id,
//                       Name = x.FileLabel,
//                       Description = x.Description,
//                       Username = x.CreatedBy,
//                       Timestamp = x.CreatedAt,
//                       //CanDelete = x.CreatedBy == username
//                       //Bank=x.Bank.Name,
//                       User = user.Name,
//                       Status = x.DocumentStatus
//                   }).ToListAsync();

//                var total = await query.CountAsync();
//                return new Tuple<ICollection<DocumentCardDto>, long>(data, total);
//            }


//        }

//        public async Task<List<DocumentCardDto>> GetDocuments(long referenceId, string username)
//        {
//            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
//            return await _context.Documents
//              .Where(q => q.ReferenceId == referenceId)
//              .Select(x => new DocumentCardDto
//              {
//                  Id = x.Id,
//                  Name = x.FileLabel,
//                  Description = x.Description,
//                  Username = x.CreatedBy,
//                  Timestamp = x.CreatedAt,
//                  User = user.Name,
//                  //CanDelete = x.CreatedBy == username && (x.DocumentStatus==DocumentStatus.Pending)
//              }).ToListAsync();
//        }

//        public async Task<DocumentCardDto> GetTemplate()
//        {
//            var record = await _context.Documents.Where(q => q.IsTemplate).Select(x => new DocumentCardDto
//            {
//                Id = x.Id,
//                Name = x.FileLabel,
//                Description = x.Description,
//                //Username = x.CreatedBy,
//                Timestamp = x.CreatedAt,
//                //CanDelete = x.CreatedBy == username
//            }).FirstOrDefaultAsync();
//            if (record == null) throw new Exception("No template was found");
//            return record;
//        }


//        private async Task<string> SaveFile(IFormFile file, string rootPath)
//        {
//            var folder = Path.Combine(rootPath, "App_Data", "Files");
//            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
//            var fileName = $"{DateTime.Now:yyMMddhhmmss}-{GeneralHelpers.TokenCode(4)}-{file.FileName}";
//            var path = Path.Combine(folder, $"{fileName}");
//            using var stream = new FileStream(path, FileMode.Create);
//            await file.CopyToAsync(stream);
//            return fileName;
//        }

//        public override async Task<bool> Delete(long id)
//        {
//            var doc = await _context.Documents.FindAsync(id);
//            if (doc.ReferenceId != null)
//            {
//                var original = await _context.Documents.FindAsync(doc.ReferenceId);
//                original.DocumentStatus = DocumentStatus.Pending;
//                _context.Documents.Update(original);
//            }
//            _context.Documents.Remove(doc);
//            _context.SaveChanges();
//            return true;
//        }

//        public async Task<List<DocumentCardDto>> GetProcessedDocuments(long id, string username)
//        {
//            var docs = new List<DocumentCardDto>();
//            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
//            var originalDoc = await _context.Documents.Where(q => q.Id == id)
//                .Select(x => new DocumentCardDto
//                {
//                    Id = x.Id,
//                    Name = x.FileLabel,
//                    Description = x.Description,
//                    //Username = x.CreatedBy,
//                    Timestamp = x.CreatedAt,
//                    //CanDelete = x.CreatedBy == username,
//                    //Bank = x.Bank.Name,
//                    ReferenceId = x.ReferenceId,
//                    User = user.Name,
//                    CanDelete = x.CreatedBy == username && (x.DocumentStatus == DocumentStatus.Pending)
//                }).FirstOrDefaultAsync(q => q.Id == id);

//            var referenceDocs = await _context.Documents.Where(q => q.ReferenceId == id)
//                  .Select(x => new DocumentCardDto
//                  {
//                      Id = x.Id,
//                      Name = x.FileLabel,
//                      Description = x.Description,
//                      Username = x.CreatedBy,
//                      Timestamp = x.CreatedAt,
//                      //CanDelete = x.CreatedBy == username,
//                      CanDelete = x.CreatedBy == username && (x.DocumentStatus == DocumentStatus.Pending),
//                      //Bank = x.Bank.Name,
//                      ReferenceId = x.ReferenceId
//                  }).OrderByDescending(x => x.Id).ToListAsync();

//            docs.Add(originalDoc);
//            if (referenceDocs.Count > 0) docs.AddRange(referenceDocs);
//            return docs;
//        }

//        public async Task<bool> SaveStats(bool IsDownload)
//        {
//            var stat = new Stat { TimeStamp = DateTime.UtcNow, Downloads = 0, Uploads = 0 };
//            var _stat = await _context.Stats.FirstOrDefaultAsync(q => q.TimeStamp.Year == stat.TimeStamp.Year);
//            if (_stat == null)
//            {
//                if (IsDownload) stat.Downloads += 1;
//                else stat.Uploads += 1;
//                await _context.Stats.AddAsync(stat);
//            }
//            else
//            {
//                if (IsDownload) _stat.Downloads += 1;
//                else _stat.Uploads += 1;
//                _context.Stats.Update(_stat);
//            }

//            _context.SaveChanges();
//            return true;
//        }

//        public async Task<StatCardDto> FetchStats()
//        {
//            var downloads = _context.Stats.Sum(q => q.Downloads);
//            var uploads = _context.Stats.Sum(q => q.Uploads);
//            var processed = _context.Documents.Where(q => q.DocumentStatus == DocumentStatus.Pending).Count();
//            var pending = _context.Documents.Where(q => q.DocumentStatus == DocumentStatus.Pending).Count();
//            var portalUsers = _context.Users.Where(q => q.View == View.Portal).Count();

//            var year = DateTime.UtcNow.Year;
//            var summary = await _context.Stats.Select(q => new TrendSummaryDto
//            {
//                Year = q.TimeStamp.Year,
//                TotalDownloads = q.Downloads,
//                TotalUploads = q.Uploads
//            }).ToListAsync();


//            var firstDate = new DateTime(year, 1, 1).Date;
//            var lastDate = new DateTime(year, 12, 31).Date;
//            var trends = await _context.Documents.Where(q => q.Timestamp >= firstDate && q.Timestamp <= lastDate)
//                .GroupBy(x => x.Timestamp.Month)
//                .Select(x => new TrendDto
//                {
//                    Month = GetMonthNames(x.Key),
//                    TotalPending = x.Count(q => q.DocumentStatus == DocumentStatus.Pending),
//                    TotalProcessed = x.Count(q => q.DocumentStatus == DocumentStatus.Complete)
//                    //Private = x.Sum(q => q.Patient.Accounts.Count(p => p.IsDefault && p.PayGroup.Type == PayGroupType.Private)),
//                }).ToListAsync();

//            var trendList = new List<TrendDto>();
//            for (var i = 1; i <= 12; i++)
//            {
//                var month = GetMonthNames(i);
//                var trend = trends.FirstOrDefault(q => q.Month == month);
//                trendList.Add(new TrendDto
//                {
//                    Month = month,
//                    TotalPending = trends.FirstOrDefault(a => a.Month == month)?.TotalPending ?? 0,
//                    TotalProcessed = trends.FirstOrDefault(a => a.Month == month)?.TotalProcessed ?? 0
//                });
//            }


//            return new StatCardDto
//            {
//                TotalProcessed = processed,
//                TotalPending = pending,
//                TotalDownloads = downloads,
//                TotalUploads = uploads,
//                TotalPortalUsers = portalUsers,
//                Summary = summary,
//                Trends = trendList
//            };

//        }

//        private static string GetMonthNames(int month)
//        {
//            return new DateTime(DateTime.Today.Year, month, 1).ToString("MMMM");
//        }


//    }

//}
