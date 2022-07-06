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
    public class ModelsVehiclesApiController : ControllerBase
    { 
        /// <summary>
        /// Add a vehicle&#x27;s model into the system.
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200">Success.</response>
        /// <response code="400">Invalid Parameter.</response>
        [HttpPost]
        [Route("/vehicleModels")]
        [Authorize(AuthenticationSchemes = BearerAuthenticationHandler.SchemeName)]
        [ValidateModelState]
        [SwaggerOperation("AddVehicleModel")]
        public virtual IActionResult AddVehicleModel([FromBody]Body6 body)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200);

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a vehicle.
        /// </summary>
        /// <param name="typeCode">typeCode of the model that needs to be removed.</param>
        /// <response code="400">Invalid typeCode.</response>
        /// <response code="404">TypeCode not found.</response>
        [HttpDelete]
        [Route("/vehicleModels/{typeCode}")]
        [Authorize(AuthenticationSchemes = BearerAuthenticationHandler.SchemeName)]
        [ValidateModelState]
        [SwaggerOperation("DeleteVehicleModel")]
        public virtual IActionResult DeleteVehicleModel([FromRoute][Required]string typeCode)
        { 
            try
            {
                VehicleModelsDAO newVehicleModelsDAO = new VehicleModelsDAO(DAOController.MyConnection);
                VehicleModels vm = newVehicleModelsDAO.Get(typeCode);

                if (vm == null) {
                    return StatusCode(404);
                }
                newVehicleModelsDAO.Delete(typeCode);
               
            }
            catch (Exception) {
                return StatusCode(400);
            }

            return StatusCode(200); 
        }

        /// <summary>
        /// Finds a model of a vehicle with a certain typeCode.
        /// </summary>
        /// <param name="typeCode">TypeCode of the model that we want to find.</param>
        /// <response code="200">Success.</response>
        /// <response code="400">Invalid TypeCode.</response>
        /// <response code="404">Model of vehicle not found.</response>
        [HttpGet]
        [Route("/vehicleModels/{typeCode}")]
        [Authorize(AuthenticationSchemes = BearerAuthenticationHandler.SchemeName)]
        [ValidateModelState]
        [SwaggerOperation("FindVehicleModel")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<VehicleModels>), description: "Success.")]
        public virtual IActionResult FindVehicleModel([FromRoute][Required]string typeCode)
        { 
            VehicleModelsDAO newVehicleModelsDAO = new VehicleModelsDAO(DAOController.MyConnection);

            try
            {
                VehicleModels vehicleModels = newVehicleModelsDAO.Get(typeCode);
                return new ObjectResult(vehicleModels);
            }
            catch (Exception) {
                return StatusCode(400);
            }
        }

        /// <summary>
        /// List all vehiclesModels.
        /// </summary>
        /// <response code="200">Success.</response>
        /// <response code="400">Operation error.</response>
        [HttpGet]
        [Route("/vehicleModels")]
        [Authorize(AuthenticationSchemes = BearerAuthenticationHandler.SchemeName)]
        [ValidateModelState]
        [SwaggerOperation("FindVehiclesModels")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<VehicleModels>), description: "Success.")]
        public virtual IActionResult FindVehiclesModels()
        { 
            VehicleModelsDAO newVehicleModelsDAO = new VehicleModelsDAO(DAOController.MyConnection);

            try
            {
                ICollection<VehicleModels> allVehicleModels = newVehicleModelsDAO.GetAll();
                return new ObjectResult(allVehicleModels);
            }
            catch (Exception) {
                return StatusCode(400);
            }
        }

        /// <summary>
        /// Updates a vehicle&#x27;s model.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="typeCode">TypeCode of the model that needs to be updated.</param>
        /// <response code="400">Invalid TypeCode.</response>
        /// <response code="404">Model of Vehicle not found.</response>
        [HttpPut]
        [Route("/vehicleModels/{typeCode}")]
        [Authorize(AuthenticationSchemes = BearerAuthenticationHandler.SchemeName)]
        [ValidateModelState]
        [SwaggerOperation("UpdateVehicleModel")]
        public virtual IActionResult UpdateVehicleModel([FromBody]VehicleModels body, [FromRoute][Required]string typeCode)
        { 
            VehicleModelsDAO newVehicleModelsDAO = new VehicleModelsDAO(DAOController.MyConnection);

            try
            {
                VehicleModels vehicleModel = newVehicleModelsDAO.Get(typeCode);

                if (vehicleModel == null || vehicleModel.TypeCode != typeCode)
                {
                    return StatusCode(404);
                }
                else newVehicleModelsDAO.Update(body);
            }
            catch (Exception) {
                return StatusCode(400);
            }

            return StatusCode(200);
        }
    }
}