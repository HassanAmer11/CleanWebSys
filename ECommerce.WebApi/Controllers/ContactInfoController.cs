using ECommerce.Application.Dtos.ContactInfoDtos;
using ECommerce.Application.IBusiness.IContactInfoBusiness;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfoController : ControllerBase
    {
        #region Fields
        private readonly IContactInfoBL _repo;
        #endregion
        #region Constractor
        public ContactInfoController(IContactInfoBL repo)
        {
            _repo = repo;
        }
        #endregion

        #region Handle Action
        [HttpGet("GetContactInfo")]
        public async Task<IActionResult> GetContactInfo()
        {
            var result = await _repo.GetFirstContactInfo();
            return Ok(result);
        }

        //[HttpGet("GetContactInfoById/{id}")]
        //public async Task<IActionResult> GetContactInfoById(int id)
        //{
        //    var result = await _repo.GetById(id);
        //    return Ok(result);
        //}

        [HttpPost("AddContactInfo")]
        public async Task<IActionResult> AddContactInfo([FromForm] ContactInfoDto dto)
        {
            var result = await _repo.AddNew(dto);
            return Ok(result);
        }

        [HttpPut("UpdateContactInfo")]
        public async Task<IActionResult> UpdateContactInfo([FromForm] ContactInfoDto dto)
        {
            var result = await _repo.Update(dto);
            return Ok(result);
        }
        //[HttpDelete("DeleteContactInfo/{id}")]
        //public async Task<IActionResult> DeleteContactInfo(int id)
        //{
        //    var result = await _repo.Delete(id);
        //    return Ok(result);
        //}
        #endregion
    }
}
