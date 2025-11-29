using AutoMapper;
using LibrarySystem.BusinessLogic.ServicesClasses;
using LibrarySystem.BusnissLogic.Dtos.BookDtos;
using LibrarySystem.BusnissLogic.Dtos.CategoriesDtos;
using LibrarySystem.BusnissLogic.ServicesInterfaces;
using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace LibrarySystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper) 
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }




        [HttpGet]
        public async Task<ActionResult<List<ReadCategoriesDto>>> GetAllAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            var CategoriesListdto = _mapper.Map<List<ReadCategoriesDto>>(categories);
            return Ok(CategoriesListdto);
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<ReadCategoriesDto>> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound($"Category with ID {id} not found");

            var categoryDto = _mapper.Map<ReadCategoriesDto>(category);
            return Ok(categoryDto);
        }




        [Authorize(Roles = "Admin,Librarian")]
        [HttpPost]

        public async Task<ActionResult<AddCategoryDto>> AddCategoryAsync(AddCategoryDto addCategoryDto)
        {
            var _Category = _mapper.Map<Category>(addCategoryDto);

            var AddedCategory = await _categoryService.AddCategoryAsync(_Category);

            var AddedCategoryDto = _mapper.Map<ReadCategoriesDto>(AddedCategory);

            return CreatedAtAction(nameof(GetCategoryByIdAsync), new { id = AddedCategoryDto.CategoryId }, AddedCategoryDto); 



        }

        [Authorize(Roles = "Admin,Librarian")]

        [HttpPut("{id}")]

        public async Task<ActionResult<UpdateCategoriesDto>> UpdateCategoryAsync(int id, UpdateCategoriesDto updatedcategoryDto)
        {
            if (id != updatedcategoryDto.CategoryId)
                return BadRequest("Category ID mismatch..");



            var categoryEntity = _mapper.Map<Category>(updatedcategoryDto);


            var UpdatedCategory = await _categoryService.UpdateCategoryAsync(id, categoryEntity);

            var dto = _mapper.Map<UpdateCategoriesDto>(UpdatedCategory);


            return Ok(dto);
        }






        [Authorize(Roles = "Admin,Librarian")]


        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteCategoryDto>> DeleteCategoryAsync(int id)
        {
           
                var deletedCategories = await _categoryService.DeleteCategoryAsync(id);

                if (deletedCategories == null)
                    return NotFound(" Category with This ID  not found.");

                return Ok(deletedCategories); 
                        
        }





        [HttpGet("search")]
        public async Task<ActionResult<List<ReadCategoriesDto>>> SearchCategoriesByNameAsync([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest("Please enter keyword.");

            var categories = await _categoryService.SearchCategoriesByNameAsync(keyword);
            var dto = _mapper.Map<List<ReadCategoriesDto>>(categories);

            return Ok(dto);
        }






        [HttpGet("count")]
        public async Task<ActionResult<int>> GetCategoriesCountAsync()
        {
            var count = await _categoryService.GetCategoriesCountAsync();
            return Ok(count);
        }





        [HttpGet("with-books")]
        public async Task<ActionResult<List<CategoryWithBooksDto>>> GetCategoryWithBooksAsync([FromQuery] string keyword)
        {
            var categories = await _categoryService.GetCategoryWithBooksAsync(keyword);

            var dto = _mapper.Map<List<CategoryWithBooksDto>>(categories);

            return Ok(dto);
        }




    }

}
