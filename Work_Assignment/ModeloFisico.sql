-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema plugandgobeyonddb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema plugandgobeyonddb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `plugandgobeyonddb` DEFAULT CHARACTER SET utf8 ;
USE `plugandgobeyonddb` ;

-- -----------------------------------------------------
-- Table `plugandgobeyonddb`.`ChargingStation`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `plugandgobeyonddb`.`ChargingStation` ;

CREATE TABLE IF NOT EXISTS `plugandgobeyonddb`.`ChargingStation` (
  `id` VARCHAR(45) NOT NULL,
  `latitude` DOUBLE NOT NULL,
  `longitude` DOUBLE NOT NULL,  
  `operatorName` VARCHAR(45) NOT NULL,
  `website` VARCHAR(45) NOT NULL,
  `status` INT(11) NOT NULL,
  `priceByActivation` DOUBLE NOT NULL,
  `priceByMinute` DOUBLE NOT NULL,
  `priceByKwh` DOUBLE NOT NULL,
  `waitingToCharge` INT(11) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `plugandgobeyonddb`.`ChargingStationWaitingValidation`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `plugandgobeyonddb`.`ChargingStationWaitingValidation` ;

CREATE TABLE IF NOT EXISTS `plugandgobeyonddb`.`ChargingStationWaitingValidation` (
  `nameChargingStation` VARCHAR(100) NOT NULL,
  `Street` VARCHAR(100) NOT NULL,
  `City` VARCHAR(45) NOT NULL,
  `LocationType` VARCHAR(45) NOT NULL,
  `AccessType` VARCHAR(45) NOT NULL,
  `Restritions` VARCHAR(100) NOT NULL,
  `AditionalInfo` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`nameChargingStation`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `plugandgobeyonddb`.`Connector`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `plugandgobeyonddb`.`Connector` ;

CREATE TABLE IF NOT EXISTS `plugandgobeyonddb`.`Connector` (
  `id` VARCHAR(45) NOT NULL,
  `status` INT(11) NOT NULL,
  `powerOutput` DOUBLE NOT NULL,
  `amps` INT(11) NOT NULL,
  `voltage` INT(11) NOT NULL,
  `phase` INT(11) NOT NULL,
  `type` INT(11) NOT NULL,
  `rate` INT(11) NOT NULL,
  `station_id` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `plugandgobeyonddb`.`CreditCard`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `plugandgobeyonddb`.`CreditCard` ;

CREATE TABLE IF NOT EXISTS `plugandgobeyonddb`.`CreditCard` (
  `username` VARCHAR(45) NOT NULL,
  `type` VARCHAR(45) NOT NULL,
  `number` VARCHAR(45) NOT NULL,
  `expireDate` DATETIME NOT NULL,
  `cvv` INT(11) NOT NULL,
  PRIMARY KEY (`number`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `plugandgobeyonddb`.`FavoriteStations`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `plugandgobeyonddb`.`FavoriteStations` ;

CREATE TABLE IF NOT EXISTS `plugandgobeyonddb`.`FavoriteStations` (
  `username` VARCHAR(45) NOT NULL,
  `station_id` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`username`, `station_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `plugandgobeyonddb`.`Permissions`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `plugandgobeyonddb`.`Permissions` ;

CREATE TABLE IF NOT EXISTS `plugandgobeyonddb`.`Permissions` (
  `username` VARCHAR(100) NOT NULL,
  `Permissions` INT(11) NOT NULL,
  PRIMARY KEY (`username`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `plugandgobeyonddb`.`Trip`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `plugandgobeyonddb`.`Trip` ;

CREATE TABLE IF NOT EXISTS `plugandgobeyonddb`.`Trip` (
  `tripId` INT(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `vehicleRegistrationNumber` VARCHAR(45) NOT NULL,
  `localStartLatitude` DOUBLE NOT NULL,
  `localStartLongitude` DOUBLE NOT NULL,
  `localEndLatitude` DOUBLE NOT NULL,
  `localEndLongitude` DOUBLE NOT NULL,
  `date` DATETIME NOT NULL,
  `duration` VARCHAR(45) NOT NULL,
  `cost` DOUBLE NOT NULL,
  `username` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`tripId`))
ENGINE = InnoDB
AUTO_INCREMENT = 2
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `plugandgobeyonddb`.`UsedStations`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `plugandgobeyonddb`.`UsedStations` ;

CREATE TABLE IF NOT EXISTS `plugandgobeyonddb`.`UsedStations` (
  `id_station` VARCHAR(300) NOT NULL,
  `trip_id` INT(11) NOT NULL,
  PRIMARY KEY (`id_station`, `trip_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `plugandgobeyonddb`.`User`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `plugandgobeyonddb`.`User` ;

CREATE TABLE IF NOT EXISTS `plugandgobeyonddb`.`User` (
  `name` VARCHAR(45) NOT NULL,
  `username` VARCHAR(45) NOT NULL,
  `nif` VARCHAR(45) NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`username`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `plugandgobeyonddb`.`Vehicle`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `plugandgobeyonddb`.`Vehicle` ;

CREATE TABLE IF NOT EXISTS `plugandgobeyonddb`.`Vehicle` (
  `registrationNumber` VARCHAR(45) NOT NULL,
  `typeCode` VARCHAR(100) NOT NULL,
  `name` VARCHAR(100) NOT NULL,
  `lastConsumptionReport` DOUBLE NOT NULL,
  `username` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`registrationNumber`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `plugandgobeyonddb`.`VehicleModels`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `plugandgobeyonddb`.`VehicleModels` ;

CREATE TABLE IF NOT EXISTS `plugandgobeyonddb`.`VehicleModels` (
  `typeCode` VARCHAR(100) NOT NULL,
  `name` VARCHAR(100) NOT NULL,
  `fastCharges` VARCHAR(45) NOT NULL,
  `levelTwoCharges` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`typeCode`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;