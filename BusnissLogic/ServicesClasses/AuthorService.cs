using AutoMapper;
using LibrarySystem.BusnissLogic.Dtos.AuthorDtos;
using LibrarySystem.BusnissLogic.ServicesInterfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.ReposInterfaces;
using Microsoft.Extensions.Caching.Memory;

namespace LibrarySystem.BusnissLogic.ServicesClasses
{
    public class AuthorService : IAuthorService 
    {
        private readonly IUnitofWorks _unitOfWork;
        private readonly IMemoryCache _cache;

        public AuthorService(IUnitofWorks unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            const string cacheKey = "All_Authors";

            if (!_cache.TryGetValue(cacheKey, out List<Author>? author))
            {
                author = await _unitOfWork.author.GetAllAsync();

                foreach (var a in author)
                {
                    if (a.Books == null)
                        a.Books = new List<Book>();
                }
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                _cache.Set(cacheKey, author, cacheOptions);
            }

            return author!;
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            var Author = await _unitOfWork.author.GetbyIdAsync(id);
            if (Author == null)
                throw new Exception($"Author with ID {id} not found.");

            return Author;
        }

        public async Task<Author> AddAuthorAsync(Author author)
        {
            

            var added = await _unitOfWork.author.AddAsync(author);
            await _unitOfWork.CompleteAsync();

            _cache.Remove("All_Authors");
            return added;
        }

        public async Task<Author> UpdateAuthorAsync(int id, Author author)
        {
            var updated = await _unitOfWork.author.UpdateAsync(author);
            await _unitOfWork.CompleteAsync();

            _cache.Remove("All_Authors");
            return updated;
        }

        public async Task<Author> DeleteAuthorAsync(int id)
        {
            var author = await _unitOfWork.author.GetbyIdAsync(id);
            if (author == null)
                throw new Exception("Category not found.");

            var deleted = await _unitOfWork.author.DeleteAsync(author);
            await _unitOfWork.CompleteAsync();

            _cache.Remove("All_Authors");
            return deleted;
        }

        public async Task<List<Author>> SearchAuthorsByNameAsync(string keyword)
        {
            var allAuthors = await GetAllAuthorsAsync();
            return allAuthors
                .Where(c => c.Name.StartsWith(keyword, StringComparison.OrdinalIgnoreCase))
                .Take(10)
                .ToList();
        }
        public async Task<IEnumerable<Author>> GetAuthorWithBooksByIdAsync(string keyword )
        {
            var allauthors = await _unitOfWork.Authors.GetAuthorWithBooksByIdAsync(keyword);
            return allauthors;
        }



       
    }

    }
