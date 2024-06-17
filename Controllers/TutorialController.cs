using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutorialApi.Models;

namespace TutorialApi.Controllers
{
    [Route("[controller]/[Action]"), ApiController]
    public class TutorialController : ControllerBase
    {
        readonly AngularDBContext _angularDBContext;
        public TutorialController(AngularDBContext angularDBContext)
        {
            _angularDBContext = angularDBContext;
        }

        [HttpGet]
        public async Task<ActionResult<Tutorial?>> tutorialById([FromQuery] int id)
        {
            return await _angularDBContext.Tutorials.Where(w => w.Id == id).FirstOrDefaultAsync();
        }

        [HttpGet]
        public async Task<ActionResult<List<Tutorial>?>> tutorialByTitle([FromQuery] string title)
        {
            return await _angularDBContext.Tutorials.Where(w => w.Title.ToLower().Contains(title.ToLower())).ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<List<Tutorial>>> tutorialGetAll()
        {
            var objs = await _angularDBContext.Tutorials.ToListAsync();
            return objs;
        }


        [HttpPost]
        public async Task<ActionResult<Tutorial?>> Addtutorial([FromBody] Tutorial obj)
        {
            await _angularDBContext.Tutorials.AddAsync(obj);
            int isOK = _angularDBContext.SaveChanges();
            Tutorial? objNew = new Tutorial();

            if (isOK > 0)
                return await _angularDBContext.Tutorials.AsNoTracking().Where(w => w.Title.Equals(obj.Title)).FirstOrDefaultAsync();
            return objNew;
        }

        [HttpPut]
        public async Task<ActionResult<Tutorial?>> Updatetutorial([FromQuery] int id, [FromBody] UpdateTutorial obj)
        {
            Tutorial? objFind = _angularDBContext.Tutorials.Where(w => w.Id == id).FirstOrDefault();
            if (objFind != null)
            {
                objFind.Title = obj.Title;
                objFind.Description = obj.Description;
                objFind.Published = obj.Published;
                _angularDBContext.Tutorials.Update(objFind);
                int isOK = await _angularDBContext.SaveChangesAsync();

                //if (isOK > 0)
                //    return objFind;
            }
            return objFind;
        }
        [HttpDelete]
        public async Task<ActionResult<Tutorial?>> Deletetutorial([FromQuery] int id)
        {
            Tutorial? objFind = _angularDBContext.Tutorials.Where(w => w.Id == id).FirstOrDefault();
            if (objFind != null)
            {
                _angularDBContext.Tutorials.Remove(objFind);
                int isOK = await _angularDBContext.SaveChangesAsync();
                //if (isOK > 0)
                //    return objFind;
            }
            return objFind;
        }

        [HttpDelete]
        public async Task<ActionResult<List<Tutorial>?>> DeleteAlltutorial([FromQuery] int id)
        {
            var objFinds = await _angularDBContext.Tutorials.ToListAsync();
            if (objFinds != null && objFinds?.Count > 0)
            {
                _angularDBContext.Tutorials.RemoveRange(objFinds);
                int isOK = await _angularDBContext.SaveChangesAsync();
                //if (isOK > 0)
                //    return objFind;
            }
            return objFinds;
        }


    }
}
