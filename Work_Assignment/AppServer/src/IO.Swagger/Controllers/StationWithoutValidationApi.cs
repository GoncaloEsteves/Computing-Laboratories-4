/*
 * PlugGoBeyond
 *
 * Rest API of the mobile app PlugGoBeyond of the project course LI IV in the program Mestrado Integrado em Engenharia Informática
 *
 * OpenAPI spec version: 1.0.0
 * Contact: a34900/a82888/a85731/a86618/a89982@alunos.uminho.pt
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using IO.Swagger.Attributes;
using IO.Swagger.Security;
using Microsoft.AspNetCore.Authorization;
using IO.Swagger.Models;
using IO.Swagger.DataAcess;

namespace IO.Swagger.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class StationWithoutValidationApiController : ControllerBase
    { 
        /// <summary>
        /// Add a Stations Waiting Validation into the system.
        /// </summary>
        /// <remarks>Inserts a new station waiting validation.</remarks>
        /// <param name="body"></param>
        /// <response code="200">Success.</response>
        /// <response code="400">Invalid Parameter.</response>
        [HttpPost]
        [Route("/chargingStationWaitingValidation")]
        [Authorize(AuthenticationSchemes = BearerAuthenticationHandler.SchemeName)]
        [ValidateModelState]
        [SwaggerOperation("AddStationWithoutValidation")]
        public virtual IActionResult AddStationWithoutValidation([FromBody]ChargingStationWaitingValidation body)
        {
            try
            {
                ChargingStationWaitingValidationDAO newCSWVDAO = new ChargingStationWaitingValidationDAO(DAOController.MyConnection);
                ChargingStationWaitingValidation cswv = newCSWVDAO.Get(body.NameChargingStation);

                if (cswv.NameChargingStation == null)
                {
                    newCSWVDAO.Put(body);
                }
                else
                {
                    newCSWVDAO.Update(body);
                }

            }
            catch (Exception)
            {
                return StatusCode(400);
            }

            return StatusCode(200);
        }

        /// <summary>
        /// Removes a station without validation.
        /// </summary>
        /// <remarks>Deletes a specific station.</remarks>
        /// <param name="nameChargingStation">name of the station that needs to be removed.</param>
        /// <response code="400">Invalid name of the station.</response>
        /// <response code="404">Name of station without validation not found.</response>
        [HttpDelete]
        [Route("/chargingStationWaitingValidation/{nameChargingStation}")]
        [Authorize(AuthenticationSchemes = BearerAuthenticationHandler.SchemeName)]
        [ValidateModelState]
        [SwaggerOperation("DeleteStationWithoutValidation")]
        public virtual IActionResult DeleteStationWithoutValidation([FromRoute][Required]string nameChargingStation)
        {
            try
            {
                ChargingStationWaitingValidationDAO newCSWVDAO = new ChargingStationWaitingValidationDAO(DAOController.MyConnection);
                ChargingStationWaitingValidation cs = newCSWVDAO.Get(nameChargingStation);

                if (cs == null)
                {
                    return StatusCode(404);
                }
                newCSWVDAO.Delete(nameChargingStation);

            }
            catch (Exception)
            {
                return StatusCode(400);
            }

            return StatusCode(200);
        }

        /// <summary>
        /// List all Charging Stations Waiting Validation.
        /// </summary>
        /// <remarks>Returns all stations that were not yet considered valid by the system.</remarks>
        /// <response code="200">Success.</response>
        /// <response code="400">Operation error.</response>
        [HttpGet]
        [Route("/chargingStationWaitingValidation")]
        [Authorize(AuthenticationSchemes = BearerAuthenticationHandler.SchemeName)]
        [ValidateModelState]
        [SwaggerOperation("FindStationWaitingValidation")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<ChargingStationWaitingValidation>), description: "Success.")]
        public virtual IActionResult FindStationWaitingValidation()
        {
            ChargingStationWaitingValidationDAO newCSWVDAO = new ChargingStationWaitingValidationDAO(DAOController.MyConnection);

            try
            {
                ICollection<ChargingStationWaitingValidation> allChargingStationsWV = newCSWVDAO.GetAll();
                return new ObjectResult(allChargingStationsWV);
            }
            catch (Exception)
            {
                return StatusCode(400);
            }
        }

        /// <summary>
        /// Finds a station by it&#x27;s name.
        /// </summary>
        /// <remarks>Given a certain valid name, a station will be returned</remarks>
        /// <param name="nameChargingStation">Name of the chargingStation that we want to find.</param>
        /// <response code="200">Success.</response>
        /// <response code="400">Operation error.</response>
        /// <response code="404">Name of station without validation not found.</response>
        [HttpGet]
        [Route("/chargingStationWaitingValidation/{nameChargingStation}")]
        [Authorize(AuthenticationSchemes = BearerAuthenticationHandler.SchemeName)]
        [ValidateModelState]
        [SwaggerOperation("FindStationWithoutValidationByName")]
        [SwaggerResponse(statusCode: 200, type: typeof(ChargingStationWaitingValidation), description: "Success.")]
        public virtual IActionResult FindStationWithoutValidationByName([FromRoute][Required]string nameChargingStation)
        { 
           ChargingStationWaitingValidationDAO newCSWVDAO = new ChargingStationWaitingValidationDAO(DAOController.MyConnection);

            try
            {
                ChargingStationWaitingValidation chargingStationsWV = newCSWVDAO.Get(nameChargingStation);
                return new ObjectResult(chargingStationsWV);
            }
            catch (Exception)
            {
                return StatusCode(400);
            }
        }

        /// <summary>
        /// Updates a Station without validation.
        /// </summary>
        /// <remarks>Given a certain valid name, a station will be updated.</remarks>
        /// <param name="body"></param>
        /// <param name="nameChargingStation">name of the station that needs to be updated.</param>
        /// <response code="400">Invalid name of the station.</response>
        /// <response code="404">Name of Station not found.</response>
        [HttpPut]
        [Route("/chargingStationWaitingValidation/{nameChargingStation}")]
        [Authorize(AuthenticationSchemes = BearerAuthenticationHandler.SchemeName)]
        [ValidateModelState]
        [SwaggerOperation("UpdateStationWithoutValidation")]
        public virtual IActionResult UpdateStationWithoutValidation([FromBody]ChargingStationWaitingValidation body, [FromRoute][Required]string nameChargingStation)
        {
            ChargingStationWaitingValidationDAO newCSWVDAO = new ChargingStationWaitingValidationDAO(DAOController.MyConnection);

            try
            {
                ChargingStationWaitingValidation cs = newCSWVDAO.Get(nameChargingStation);

                if (cs == null || cs.NameChargingStation != nameChargingStation)
                {
                    return StatusCode(404);
                }
                else newCSWVDAO.Update(body);
            }
            catch (Exception)
            {
                return StatusCode(400);
            }

            return StatusCode(200);
        }
    }
}
