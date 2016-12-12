using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Promact.Oauth.Server.Models;
using Promact.Oauth.Server.Models.ApplicationClasses;
using Promact.Oauth.Server.Repository.ConsumerAppRepository;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Promact.Oauth.Server.ExceptionHandler;

namespace Promact.Oauth.Server.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class ConsumerAppController : Controller
    {
        #region "Private Variable(s)"
        private readonly IConsumerAppRepository _consumerAppRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region "Constructor"
        public ConsumerAppController(IConsumerAppRepository iConsumerAppRepository, UserManager<ApplicationUser> userManager)
        {
            _consumerAppRepository = iConsumerAppRepository;
            _userManager = userManager;
        }

        #endregion

        #region Public Methods


        /**
        * @api {post} api/consumerapp 
        * @apiVersion 1.0.0
        * @apiName AddConsumerAppAsync
        * @apiGroup ConsumerApp
        * @apiParam {object} consumerAppsAc  object
        * @apiParamExample {json} Request-Example:  
        *  {
        *     "Name":"ProjectName",
        *     "Description":"True",
        *     "CallbackUrl":"1",
        *  }      
        * @apiSuccessExample {json} Success-Response:
        * HTTP/1.1 200 OK 
        * {
        *    true
        * }
        * @apiError ConsumerAppNameIsAlreadyExists The ConsumerApp Name is Already Exists
        * @apiErrorExample {json} Error-Response:
        * HTTP/1.1 400 Bad Request 
        * {
        *   "error": "ConsumerAppNameIsAlreadyExists"
        * }
        */
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddConsumerAppAsync([FromBody]ConsumerAppsAc consumerAppsAc)
        {
            try
            {
                consumerAppsAc.CreatedBy = _userManager.GetUserId(User);
                return Ok(await _consumerAppRepository.AddConsumerAppsAsync(consumerAppsAc));
            }
            catch (ConsumerAppNameIsAlreadyExists)
            {
                return BadRequest();
            }
        }


        /**
        * @api {get} api/consumerapp 
        * @apiVersion 1.0.0
        * @apiName GetConsumerAppsAsync
        * @apiGroup ConsumerApp
        * @apiSuccessExample {json} Success-Response:
        * HTTP/1.1 200 OK 
        * {
        *   {
        *   "Name":"ProjectName",
        *   "Description":"SlackChannelName",
        *   "CallbackUrl ":"localhost:35716/oAuth/RefreshToken",
        *   "AuthId ":"XyzDemo123DSQWE",
        *   "AuthSecret ":"XyzWERTCDSwasaswre232_e322"
        *   }
        * }
        */
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetConsumerAppsAsync()
        {
            List<ConsumerApps> listOfApps = await _consumerAppRepository.GetListOfConsumerAppsAsync();
            return Ok(listOfApps);
        }


        /**
       * @api {get} api/consumerapp/:id 
       * @apiVersion 1.0.0
       * @apiName GetConsumerAppByIdAsync
       * @apiGroup ConsumerApp
       * @apiParam {int} id
       * @apiParamExample {json} Request-Example:  
       *        {
       *            "id":"1"
       *        }     
       * @apiSuccessExample {json} Success-Response:
       * HTTP/1.1 200 OK 
       * {
       *  {
       *   "Name":"ProjectName",
       *   "Description":"SlackChannelName",
       *   "CallbackUrl ":"localhost:35716/oAuth/RefreshToken",
       *   "AuthId ":"XyzDemo123DSQWE",
       *   "AuthSecret ":"XyzWERTCDSwasaswre232_e322"
       *   }
       * }
       * @apiError ConsumerAppNotFound The id of the ConsumerApp was not found.
       * @apiErrorExample {json} Error-Response:
       * HTTP/1.1 404 Not Found
       * {
       *   "error": "ConsumerAppNotFound"
       * }
       */
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetConsumerAppByIdAsync(int id)
        {
            try
            {
                ConsumerApps consumerApps = await _consumerAppRepository.GetConsumerAppByIdAsync(id);
                return Ok(consumerApps);
            }
            catch (ConsumerAppNotFound)
            {
                return NotFound();
            }
        }

        /**
        * @api {put} api/consumerapp 
        * @apiVersion 1.0.0
        * @apiName UpdateConsumerAppAsync
        * @apiGroup ConsumerApp
        * @apiParam {object} consumerAppsAc object
        * @apiParamExample {json} Request-Example: 
        *    "Id":"1", 
        *       {
        *             "Id":"1", 
        *             "Name":"ProjectName",
        *             "Description":"True",
        *             "CallbackUrl":"1",
        *             "AuthSecret":"ABCDEEADweesd",
        *             "AuthId":"ABCDE123XYZED45awadddfgdAWWEWFsfsFF",
        *             
        *       } 
        * @apiSuccessExample {json} Success-Response:
        * HTTP/1.1 200 OK 
        * {
        *   true
        * }
        * @apiError ConsumerAppNameIsAlreadyExists The ConsumerApp Name is Already Exists
        * @apiErrorExample {json} Error-Response:
        * HTTP/1.1 400 Bad Request
        * {
        *   "error": "ConsumerAppNameIsAlreadyExists"
        * }
        */
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateConsumerAppAsync(int id, [FromBody]ConsumerAppsAc consumerAppsAc)
        {
            try
            {
                ConsumerApps consumerApp = await _consumerAppRepository.GetConsumerAppByIdAsync(id);
                consumerApp.Name = consumerAppsAc.Name;
                consumerApp.CallbackUrl = consumerAppsAc.CallbackUrl;
                consumerApp.Description = consumerAppsAc.Description;
                consumerApp.UpdatedBy = _userManager.GetUserId(User);
                consumerApp.UpdatedDateTime = DateTime.Now;
                return Ok(await _consumerAppRepository.UpdateConsumerAppsAsync(consumerApp));
            }
            catch (ConsumerAppNameIsAlreadyExists)
            {
                return BadRequest();
            }
        }

        #endregion
    }
}