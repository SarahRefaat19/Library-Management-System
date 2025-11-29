using LibrarySystem.BusnissLogic.Dtos.BookDtos;
using LibrarySystem.BusnissLogic.ServicesInterfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Transactions;
using static System.Net.Mime.MediaTypeNames;

namespace LibrarySystem.BusnissLogic.ServicesClasses
{
    public class BookServicecs : IBookService
    {
        private readonly IbookRepository _bookRepository;
        private readonly IMemoryCache _cache; 
        private readonly FileUploadService _fileUploadService;


        public BookServicecs(IbookRepository bookRepository , IMemoryCache cache , FileUploadService  fileUploadService)
        {
            _bookRepository = bookRepository;
            _cache = cache;
            _fileUploadService = fileUploadService;

        }


        public async Task<List<Book>> GetAllBooksAsync()=> await _bookRepository.GetAllAsync();

        public async Task<Book?> GetBookByIdAsync(int id)
        {

            if(_cache.TryGetValue(id , out Book? Cashedbook))
                return Cashedbook;



            var book = await _bookRepository.GetbyIdAsync(id);


            if (book != null)
                _cache.Set(id,book, TimeSpan.FromMinutes(15));
                 return book;

        }


        public async Task<Book> AddBookAsync(Book book)
        {
            if (string.IsNullOrEmpty(book.Title))
                throw new ArgumentNullException(" Error: You Should to Enter Book Title ");

            if (book.ImageUrl != null)
            {
                var imagePath = await _fileUploadService.UploadImageAsync(book.ImageFile);
                book.ImageUrl = imagePath; 
            }

            return await _bookRepository.AddAsync(book);

        }

        public async Task<Book> UpdateBookAsync(int id , Book book)
        {

            var ExistingBook = await _bookRepository.GetbyIdAsync(id);

            if (ExistingBook == null)
                throw new Exception("This Book is  Not Found ");
            return  await _bookRepository.UpdateAsync(book);
        }

        public async Task<Book> DeleteBookAsync(int id)
        {
            var existingBook= await _bookRepository.GetbyIdAsync(id);

            if (existingBook == null)
                throw new Exception("This Book is  Not Found ");
             return await _bookRepository.DeleteAsync(existingBook);
        }

        public async Task<List<Book>> SearchBooksAsync(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                throw new ArgumentNullException(" Not Found ");


            return await _bookRepository.GetAllAsync();

        }
        public async Task<bool> IsBookAvailableAsync(int id)
        {
            var book = await _bookRepository.GetbyIdAsync(id);
            if (book == null)
                throw new Exception("This book is Not found ");


           return book.IsAvailable;

        }
    }
}
