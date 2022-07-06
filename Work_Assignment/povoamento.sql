-- ************* Script de Povoamento da Base de Dados *************

-- Base de Dados de trabalho
Use plugandgobeyonddb;

-- Povoamento da tabela "chargingstation"
INSERT INTO chargingstation
	(id, latitude, longitude, operatorName, website, status, priceByActivation, priceByMinute, priceByKwh, waitingToCharge)
    VALUES
		(1,41.56131,-8.393804, 'DI-Uminho','www.uminho.pt',1,0.05,0.05,0.05,0);

-- Povoamento da tabela "chargingstationwaitingvalidation"
INSERT INTO chargingstationwaitingvalidation
	(nameChargingStation, Street, City, LocationType, AccessType, Restritions,AditionalInfo)
    VALUES
		('Uminho-Azurem','Rua de Azurem','Guimaraes','cidade','facil','nenhuma','nenhuma');

-- Povoamento da tabela "connector"
INSERT INTO connector
	(id, status, powerOutput, amps, voltage, phase, type, rate, station_id)
    VALUES
		('1',1,66.99,15,23,3,3,1,'1');

-- Povoamento da tabela "creditcard"
INSERT INTO creditcard
	(username,type,number,expireDate,cvv)
    VALUES
		('joel','gold',123456789,'2100-07-23 00:00:00',999);
        
-- Povoamento da tabela "favoritestations"
INSERT INTO favoritestations
	(username,station_id)
    VALUES
		('joel','1');
        
-- Povoamento da tabela "permissions"
INSERT INTO permissions
	(username,permissions)
    VALUES
		('joel',1);
        
-- Povoamento da tabela "trip"
INSERT INTO trip
	(tripId,name,vehicleRegistrationNumber, localStartLatitude,localStartLongitude, localEndLatitude,localEndLongitude, date, duration, cost, username)
    VALUES
		(1,'demonstracao','00-OO-00', 41.56131,-8.393804,41.56137,-8.393807,'2020-05-06 11:35:00','2019-02-23 20:02:21.550',1.0,'joel');
        
        
-- Povoamento da tabela "usedstations"
INSERT INTO usedstations
	(id_station,trip_id)
    VALUES
		('1',1);
        
-- Povoamento da tabela "user"
INSERT INTO user
	(name,username,nif,password,email)
    VALUES
		('Joel','joel','123456789','123','email');
        
-- Povoamento da tabela "vehicle"
INSERT INTO vehicle
	(registrationNumber,typeCode,name,lastConsumptionReport,username)
    VALUES
		('00-OO-00','3standard','Tesla;Model 3;Standard Range RWD (alpha)',0.02,'joel');


-- Povoamento da tabela "vehiclemodels"

INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('3standard','Tesla;Model 3;Standard Range RWD (alpha)','SC,ccs,tesla_ccs,chademo','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:m3:19:bt36:none','Tesla;Model 3;Standard Range Plus RWD','SC,ccs,tesla_ccs,chademo','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:my:19:bt35:none','Tesla;Model Y;Standard Range (alpha)','SC,ccs,tesla_ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:m3:18:bt37mid:19inch','Tesla;Model 3;Mid Range RWD 19"','SC,ccs,tesla_ccs,chademo','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('3mid','Tesla;Model 3;Mid Range RWD Aero 18"','SC,ccs,tesla_ccs,chademo','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:ms:19:btx5ru:range','Tesla;Model S;2019;Range Upgrade (Raven) Standard Range (alpha)','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:mx:19:btx5ru:range','Tesla;Model X;2019;Range Upgrade (Raven) Standard Range (alpha)','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:my:19:bt37:none','Tesla;Model Y;Long Range (alpha)','SC,ccs,tesla_ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('3long_awd19','Tesla;Model 3;Long Range AWD 19"','SC,ccs,tesla_ccs,chademo','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('3long_awd','Tesla;Model 3;Long Range AWD Aero 18"','SC,ccs,tesla_ccs,chademo','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('3p20','Tesla;Model 3;Long Range Performance 20"','SC,ccs,tesla_ccs,chademo','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:m3:19:bt37:perfminus','Tesla;Model 3;Long Range Performance Minus 18"','SC,ccs,tesla_ccs,chademo','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:m3:19:bt37:perfminus19','Tesla;Model 3;Long Range Performance Minus 19"','SC,ccs,tesla_ccs,chademo','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('3long19','Tesla;Model 3;Long Range RWD 19"','SC,ccs,tesla_ccs,chademo','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('3long','Tesla;Model 3;Long Range RWD Aero 18"','SC,ccs,tesla_ccs,chademo','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('s60','Tesla;Model S;2012-2018;60','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('news60','Tesla;Model S;2012-2018;60 (75)','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('s60d','Tesla;Model S;2012-2018;60D','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('news60d','Tesla;Model S;2012-2018;60D (75)','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('s70','Tesla;Model S;2012-2018;70','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('s70d','Tesla;Model S;2012-2018;70D','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('s75btx8','Tesla;Model S;2012-2018;75 (85 kWh)','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:ms:18:btx5_limited:none','Tesla;Model S;2012-2018;75;Limited Supercharging','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('s75','Tesla;Model S;2012-2018;75;Normal','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('s75btx8d','Tesla;Model S;2012-2018;75D (85 kWh)','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:ms:18:btx5_limited:dual','Tesla;Model S;2012-2018;75D;Limited Supercharging','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('s75d','Tesla;Model S;2012-2018;75D;Normal','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('s85','Tesla;Model S;2012-2018;85','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('s85d','Tesla;Model S;2012-2018;85D','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('s90d','Tesla;Model S;2012-2018;90D','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:ms:18:btx4_limited:dual','Tesla;Model S;2012-2018;90D;Limited Supercharging','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('sp85','Tesla;Model S;2012-2018;P85','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('sp85d','Tesla;Model S;2012-2018;P85D','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:ms:18:btx4_limited:perf','Tesla;Model S;2012-2018;P90D;Limited Supercharging','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('sp90d','Tesla;Model S;2012-2018;P90D;Normal','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:ms:19:btx6:range','Tesla;Model S;2019;Range Upgrade (Raven) Long Range (beta)','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('x60d','Tesla;Model X;2015-2018;60D (75)','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('x75btx8d','Tesla;Model X;2015-2018;75D (85 kWh)','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:mx:18:btx5_limited:dual','Tesla;Model X;2015-2018;75D;Limited Supercharging','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('x75d','Tesla;Model X;2015-2018;75D;Normal','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:mx:18:btx4_limited:dual','Tesla;Model X;2015-2018;90D;Limited Supercharging','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('x90d','Tesla;Model X;2015-2018;90D;Normal','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:mx:18:btx4_limited:perf','Tesla;Model X;2015-2018;P90D;Limited Supercharging','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('xp90d','Tesla;Model X;2015-2018;P90D;Normal','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:mx:19:btx6:range','Tesla;Model X;2019;Range Upgrade (Raven) Long Range (beta)','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:my:19:bt37:awd','Tesla;Model Y;Long Range AWD (alpha)','SC,ccs,tesla_ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:ms:18:btx6_limited:dual','Tesla;Model S;2012-2018;100D;Limited Supercharging','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('s100d','Tesla;Model S;2012-2018;100D;Normal','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:ms:18:btx6_limited:perf','Tesla;Model S;2012-2018;P100D;Limited Supercharging','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('sp100d','Tesla;Model S;2012-2018;P100D;Normal','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:ms:19:btx6:range-perf','Tesla;Model S;2019;Range Upgrade (Raven) Performance (beta)','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:mx:18:btx6_limited:dual','Tesla;Model X;2015-2018;100D;Limited Supercharging','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('x100d','Tesla;Model X;2015-2018;100D;Normal','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:mx:18:btx6_limited:perf','Tesla;Model X;2015-2018;P100D;Limited Supercharging','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('xp100d','Tesla;Model X;2015-2018;P100D;Normal','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:mx:19:btx6:range-perf','Tesla;Model X;2019;Range Upgrade (Raven) Performance (beta)','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:my:19:bt37:perf','Tesla;Model Y;Performance (alpha)','SC,ccs,tesla_ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:ms:19:btx9:none','Tesla;Model S;2019;Early 2019 Standard Range','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:mx:19:btx6lim:none','Tesla;Model X;2019;Early 2019 Standard Range','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:ms:19:btx6:none','Tesla;Model S;2019;Early 2019 Long Range 100D','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:mx:19:btx6:none','Tesla;Model X;2019;Early 2019 Long Range 100D','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:ms:19:btx6:perf21','Tesla;Model S;2019;Early 2019 Performance P100D','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:mx:19:btx6:perf22','Tesla;Model X;2019;Early 2019 Performance P100D','SC,chademo,ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:ct:23:sr:s','Tesla;Cybertruck;Single Motor RWD(alpha)','SC,ccs,tesla_ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:ct:23:mr:d','Tesla;Cybertruck;Dual Motor AWD(alpha)','SC,ccs,tesla_ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('tesla:ct:23:lr:t','Tesla;Cybertruck;Tri Motor AWD(alpha)','SC,ccs,tesla_ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('aptera:gemini:2021:100:none','Aptera;100 kWh (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('aptera:gemini:2021:60:none','Aptera;60 kWh (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('audi:etron:20:71:50q','Audi;E-Tron;50 Quattro (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('audi:etron:20:71:50sb','Audi;E-Tron;50 Quattro Sportback (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('audi:etron:19:95:other','Audi;E-Tron;55 Quattro (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('audi:etron:19:95:ml','Audi;E-Tron;55 Quattro (Mirrorless) (alpha)','ccs','type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('audi:etron:19:95:sb','Audi;E-Tron;55 Quattro Sportback (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('audi:etron:19:95:sb-ml','Audi;E-Tron;55 Quattro Sportback (Mirrorless) (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('bmw:i3:14:22:other','BMW;i3 60 Ah (alpha)','ccs','type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('bmw:i3:16:33:other','BMW;i3 94 Ah (alpha)','ccs','type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('byton:mbyte:20:72','Byton;M-Byte 72 (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('byton:mbyte:20:95','Byton;M-Byte 95 (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('chevy:bolt:17:60:other','Chevrolet;Bolt EV;2017-2019','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('chevy:bolt:20:66','Chevrolet;Bolt EV;2020+ (beta)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('chevy:spark:14:19:other','Chevrolet;Spark EV (alpha)','ccs','j1772');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('citroen:czero:09:16:other','Citroën;C-Zero;2009-2012 (16kWh) (alpha)','chademo','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('citroen:czero:12:14:other','Citroën;C-Zero;2012+ (16kWh)(alpha)','chademo','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('citroen:berlingo:15:20','Citroën;E-Berlingo (alpha)','chademo','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('ds:3crossback:20:48','DS;3 Crossback E-Tense (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('fiat:500e:24:13','Fiat;500e (alpha)','j1772,type2,type2_cable','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('ford:focus:12:23','Ford;Focus Electric;2012-2016 (alpha)','j1772','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('ford:focus:17:34','Ford;Focus Electric;2017-2018 (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('ford:mach-e:21:er:awd','Ford;Mustang Mach-E;Extended Range;AWD (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('ford:mach-e:21:er:gt','Ford;Mustang Mach-E;Extended Range;GT (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('ford:mach-e:21:er:rwd','Ford;Mustang Mach-E;Extended Range;RWD (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('ford:mach-e:21:sr:awd','Ford;Mustang Mach-E;Standard Range;AWD (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('ford:mach-e:21:sr:rwd','Ford;Mustang Mach-E;Standard Range;RWD (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('hd:livewire:20:16','Harley-Davidson;LiveWire (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('honda:e:20:36','Honda;E (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('hyundai:ioniq:17:28:other','Hyundai;Ioniq;28kWh','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('hyundai:ioniq:19:38:other','Hyundai;Ioniq;38kWh (beta)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('hyundai:kona:19:39:other','Hyundai;Kona;39 kWh (beta)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('hyundai:kona:19:64:other','Hyundai;Kona;64kWh','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('jaguar:ipace:19:90:other','Jaguar;I-Pace (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('kia:niro:19:39:other','Kia;Niro;39 kWh (beta)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('kia:niro:19:64:other','Kia;Niro;64kWh','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('kia:soul:14:27:other','Kia;Soul EV;2016-17 27kWh','chademo','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('kia:soul:18:30:other','Kia;Soul EV;2018-19 30kWh','chademo','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('kia:soul:19:39','Kia;Soul EV;e-Soul 39 kWh (beta)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('kia:soul:19:64:other','Kia;Soul EV;e-Soul 64 kWh','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('lightyear:one:20:60','Lightyear;One (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('mazda:mx-30:21:32','Mazda;MX-30 (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('mercedes:b:15:28:250e','Mercedes-Benz;B 250E (alpha)','j1772,type2,type2_cable','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('mercedes:eqc:19:80:other','Mercedes-Benz;EQC (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('mercedes:esprinter:20:41:cargo','Mercedes-Benz;eSprinter;41kWh (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('mercedes:esprinter:20:55:cargo','Mercedes-Benz;eSprinter;55kWh (alpha)','ccs','type2,type2_cable,j1772');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('mercedes:evito:20:41:cargo','Mercedes-Benz;eVito;41kWh (alpha)','type2,type2_cable','type2,type2_cable,j1772');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('mg:zs:20:45:exc','MG;ZS EV (alpha)','ccs','type2,type2_cable,j1772');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('mini:cooper:20:29','Mini;Cooper Se (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('mitsubishi:imiev:09:16:other','Mitsubishi;iMiev (16kWh)(alpha)','chademo','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('nissan:env200:12:24:none','Nissan;e-NV200;24kWh (beta)','chademo','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('nissan:env200:16:30:none','Nissan;e-NV200;30 kWh (beta)','chademo','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('nissan:env200:18:40:none','Nissan;e-NV200;40 kWh (beta)','chademo','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('nissan:leaf:12:24:other','Nissan;Leaf;2012-2015 24kWh','chademo','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('nissan:leaf:16:30:other','Nissan;Leaf;2016-2018 30kWh','chademo','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('nissan:leaf:18:40:other','Nissan;Leaf;2018+ 40kWh','chademo','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('nissan:leaf:19:62:eplus','Nissan;Leaf;2018+ e+ 62kWh (beta)','chademo','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('opel:ampera-e:17:60:other','Opel;Ampera-E','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('opel:corsae:20:50','Opel;Corsa-E (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('peugeot:e2008:20:48','Peugeot;e-2008 SUV (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('peugeot:e208:20:50','Peugeot;e-208 (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('peugeot:ion:09:16:other','Peugeot;iOn;2009-2012 (16kWh)(alpha)','chademo','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('peugeot:ion:12:14:other','Peugeot;iOn;2012+(14.5kWh)(alpha)','chademo','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('peugeot:partner:15:20','Peugeot;Partner (alpha)','chademo','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('polestar:2:20:75','Polestar;Polestar 2 (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('porsche:taycan:20:80:base','Porsche;Taycan;4S (80 kWh) (alpha)','ccs','type2,type2_cable,j1772');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('porsche:taycan:20:80:base:50kw','Porsche;Taycan;4S (80 kWh);w/o HPC (alpha)','ccs','type2,type2_cable,j1772');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('porsche:taycan:20:96:base','Porsche;Taycan;4S (93 kWh) (alpha)','ccs','type2,type2_cable,j1772');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('porsche:taycan:21:80:ct','Porsche;Taycan;Cross Turismo (80 kWh) (alpha)','ccs','type2,type2_cable,j1772');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('porsche:taycan:21:96:ct','Porsche;Taycan;Cross Turismo S (93 kWh) (alpha)','ccs','type2,type2_cable,j1772');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('porsche:taycan:20:96:turbo','Porsche;Taycan;Turbo (93 kWh) (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('porsche:taycan:20:96:turbo:50kw','Porsche;Taycan;Turbo (93 kWh);w/o HPC (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('porsche:taycan:20:96:4s','Porsche;Taycan;Turbo S (93 kWh) (alpha)','ccs','type2,type2_cable,j1772');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('porsche:taycan:20:96:4s:50kw','Porsche;Taycan;Turbo S (93 kWh);w/o HPC (alpha)','ccs','type2,type2_cable,j1772');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('renault:zoe:q210:22:other','Renault;Zoe;22 kWh Q210 (alpha)','type2,type2_cable','type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('renault:zoe:r240:22:other','Renault;Zoe;22 kWh R240 (alpha)','type2,type2_cable','type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('renault:zoe:q90:40:other','Renault;Zoe;Z.E. 40 Q90 (alpha)','type2,type2_cable','type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('renault:zoe:r110:40:other','Renault;Zoe;Z.E. 40 R110 (alpha)','type2,type2_cable','type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('renault:zoe:r90:40:other','Renault;Zoe;Z.E. 40 R90 (alpha)','type2,type2_cable','type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('renault:zoe:20:52:r110','Renault;Zoe;Z.E. 50 R110 (alpha)','ccs','type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('renault:zoe:20:52:r135','Renault;Zoe;Z.E. 50 R135 (alpha)','ccs','type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('rivian:r1s:20:105:other','Rivian;R1S;105 (alpha)','ccs','type2,type2_cable,j1772');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('rivian:r1s:20:135:other','Rivian;R1S;135 (alpha)','ccs','type2,type2_cable,j1772');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('rivian:r1s:20:180:other','Rivian;R1S;180 (alpha)','ccs','type2,type2_cable,j1772');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('rivian:r1t:20:105:other','Rivian;R1T;105 (alpha)','ccs','type2,type2_cable,j1772');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('rivian:r1t:20:135:other','Rivian;R1T;135 (alpha)','ccs','type2,type2_cable,j1772');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('rivian:r1t:20:180:other','Rivian;R1T;180 (alpha)','ccs','type2,type2_cable,j1772');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('volkswagen:eup:20:32:seat','SEAT;Mii electric (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('volkswagen:eup:20:32:skoda','Skoda;CITIGOe iV 2020 (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('smart:ed3:12:18:fortwo','Smart;ED3;Fortwo (beta)','type2,type2_cable','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('smart:ed3:12:18:fortwo-3.7','Smart;ED3;Fortwo 3.7 kW (beta)','type2,type2_cable','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('smart:ed4:17:18:fortwo','Smart;ED4;Fortwo (beta)','type2,type2_cable','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('smart:ed4:17:18:fortwo-4.6','Smart;ED4;Fortwo 4.6 kW (beta)','type2,type2_cable','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('smart:ed4:17:18:fortwo-7.2','Smart;ED4;Fortwo 7.2 kW (beta)','type2,type2_cable','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('sono:sion:20:35','Sono;Sion (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('roadster2','Tesla;Roadster 2020 (alpha)','SC,ccs,tesla_ccs','type2,type2_cable,destcharger');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('twike:twike5:21:30:none','Twike;Twike-5 30 kWh (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('volkswagen:egolf:12:26:other','Volkswagen;e-Golf;26 kWh (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('volkswagen:egolf:17:36:other','Volkswagen;e-Golf;36 kWh (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('volkswagen:eup:13:26:other','Volkswagen;e-Up!;2013-2019 (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('volkswagen:eup:20:32','Volkswagen;e-Up!;2020 (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('volkswagen:id3:20:45:sr','Volkswagen;ID.3;Pure 45kWh (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('volkswagen:id4:21:85','Volkswagen;ID.4 - Crozz (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('volkswagen:id7:23:98:lr','Volkswagen;ID.7 - Buzz (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('volvo:xc40:20:75','Volvo;XC40 (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('zero:sds:yy:zf7.2','Zero;SDS ZF 7.2 (alpha)','j1772,type2,type2_cable','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('zero:sds:yy:zf7.2:pt','Zero;SDS ZF 7.2 + PT(alpha)','j1772,type2,type2_cable','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('zero:sds:yy:zf12.5','Zero;SDS ZF12.5 (alpha)','j1772,type2,type2_cable','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('zero:sds:yy:zf12.5:pt','Zero;SDS ZF12.5 + PT (alpha)','j1772,type2,type2_cable','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('zero:sds:yy:zf13.3','Zero;SDS ZF13.3 (alpha)','j1772,type2,type2_cable','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('zero:sds:yy:zf13.3:pt','Zero;SDS ZF13.3 + PT (alpha)','j1772,type2,type2_cable','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('zero:sds:yy:zf14.4','Zero;SDS ZF14.4 (alpha)','j1772,type2,type2_cable','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('zero:sds:yy:zf14.4:pt','Zero;SDS ZF14.4 + PT (alpha)','j1772,type2,type2_cable','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('bmw:i3:19:38:other','BMW;i3 120 Ah (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('bmw:i3s:19:38:other','BMW;i3s 120 Ah (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('volkswagen:id3:20:58:mr:seat','SEAT;el-Born (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('smart:ed4:17:18:forfour','Smart;ED4;Fortfour (beta)','type2,type2_cable','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('smart:ed4:17:18:forfour-4.6','Smart;ED4;Fortfour 4.6 kW (beta)','type2,type2_cable','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('smart:ed4:17:18:forfour-7.2','Smart;ED4;Fortfour 7.2 kW (beta)','type2,type2_cable','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('volkswagen:id3:20:58:mr','Volkswagen;ID.3;Pro 58kWh (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('bmw:i4:22:80','BMW;i4 (alpha)','ccs','j1772,type2,type2_cable');
INSERT INTO vehiclemodels(typeCode,name,fastCharges,levelTwoCharges) VALUES ('volkswagen:id3:20:77:lr','Volkswagen;ID.3;Pro S 77kWh (alpha)','ccs','j1772,type2,type2_cable');
