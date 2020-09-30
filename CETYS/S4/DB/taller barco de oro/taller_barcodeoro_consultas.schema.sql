CREATE DATABASE  IF NOT EXISTS `taller_barcodeoro` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `taller_barcodeoro`;
-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: localhost    Database: taller_barcodeoro
-- ------------------------------------------------------
-- Server version	8.0.19

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `carro`
--

DROP TABLE IF EXISTS `carro`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `carro` (
  `carro_id` int NOT NULL,
  `cliente_id` int NOT NULL,
  `taller_id` int NOT NULL,
  `marca` varchar(15) NOT NULL,
  `modelo` varchar(25) NOT NULL,
  `ano` year NOT NULL,
  `color` varchar(25) NOT NULL,
  `placas` varchar(12) NOT NULL,
  `salida` date DEFAULT NULL,
  PRIMARY KEY (`carro_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `carro`
--

LOCK TABLES `carro` WRITE;
/*!40000 ALTER TABLE `carro` DISABLE KEYS */;
INSERT INTO `carro` VALUES (1,1,2,'Ford','Fiesta',2010,'Rojo','AKV-19-07','2020-04-15'),(2,2,1,'Chevrolet','Aveo',2006,'Blanco','AJS-54-32','2020-04-16'),(3,3,5,'Volskwagen','Jetta',2010,'Negro','ALM-09-87','2020-04-16'),(4,4,3,'Ford','Lobo',2011,'Azul','AJF-43-45','2020-04-16'),(5,5,4,'Ford','F-150',2006,'Blanco','ATE-12-34','2020-02-17'),(6,6,1,'Dodge','Avenger',2009,'Negro','AEP-65-70','2020-02-17'),(7,7,4,'Toyota','Camry',2013,'Verde','APU-48-56',NULL),(8,8,3,'Ford','Explorer',2000,'Gris','APN-43-61','2020-03-21'),(9,9,5,'Chevrolet','Silverado',2015,'Gris','AKI-18-94','2020-04-23'),(10,10,2,'GMC','Sierra',2016,'Negro','AKR-03-14',NULL),(11,11,4,'Chevrolet','Camaro',2014,'Amarillo','ARO-76-00','2020-04-27'),(12,12,1,'Ford','Focus',2007,'Rojo','ARN-54-71','2020-05-10'),(13,13,5,'Honda','Civic',2010,'Azul','ANE-51-04',NULL),(14,14,2,'Honda','HR-V',2017,'Rojo','ARB-52-85','2020-05-15'),(15,15,3,'Mazda','Mazda3',2018,'Gris','AKP-41-79','2020-05-16'),(16,5,4,'Ford','F-250',2006,'Blanco','ATE-12-34','2020-05-19');
/*!40000 ALTER TABLE `carro` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cliente`
--

DROP TABLE IF EXISTS `cliente`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cliente` (
  `cliente_id` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(25) NOT NULL,
  `fecha_de_nacimiento` date NOT NULL,
  `telefono` varchar(14) NOT NULL,
  `direccion` varchar(45) NOT NULL,
  PRIMARY KEY (`cliente_id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cliente`
--

LOCK TABLES `cliente` WRITE;
/*!40000 ALTER TABLE `cliente` DISABLE KEYS */;
INSERT INTO `cliente` VALUES (1,'David','1991-02-15','6646283720','Calle Urbanistas 2430'),(2,'Isaac','1990-05-20','6641234567','Calle Lomas Lugubres 5491'),(3,'Christian','1985-10-12','6647654321','Avenida del Sol 1260'),(4,'Diego','1992-06-09','6630987654','Terrenos tormentosos 0915'),(5,'Adolfo','1988-01-19','6641029384','Alameda afligida 4941'),(6,'Adrian','1993-04-21','6641230987','Finca frenesi 1234'),(7,'Kevin','1984-02-01','6644857610','Arenas ardientes 6592'),(8,'Juan','1995-09-17','6645768475','Muelles mugrientos 5931'),(9,'Pablo','1981-10-02','6631237654','Parque placentero 9581'),(10,'Ruben','1956-02-07','6640980987','Setos sagrados 3718'),(11,'Oscar','1959-12-23','6645465721','Ciudad comercio 7814'),(12,'Miguel','1965-11-29','6641437698','Avenida siempre viva 7261'),(13,'Alejandro','1961-09-03','6641436587','Pantano pringoso 9186'),(14,'Manuel','1969-08-25','6643476090','Balsa botin 8174'),(15,'Mizael','1998-06-15','6646571211','Pisos picados 6153');
/*!40000 ALTER TABLE `cliente` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `departamento`
--

DROP TABLE IF EXISTS `departamento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `departamento` (
  `departamento_id` int NOT NULL,
  `nombre_dpto` varchar(45) NOT NULL,
  `descripcion` varchar(45) NOT NULL,
  `super_id` int NOT NULL,
  PRIMARY KEY (`departamento_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `departamento`
--

LOCK TABLES `departamento` WRITE;
/*!40000 ALTER TABLE `departamento` DISABLE KEYS */;
INSERT INTO `departamento` VALUES (1,'Carroceria y pintura','Reparacion de exteriores del vehiculo',1),(2,'Electrico','Problema de indole electrico',10),(3,'Alineacion, balanceo y suspension','Todo lo relacionado a cambio de llantas',5),(4,'Mecanico','Problemas generales del carro',3),(5,'Refaccionaria','Tienda de reparación de aparatos mecánicos',8);
/*!40000 ALTER TABLE `departamento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `empleado`
--

DROP TABLE IF EXISTS `empleado`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `empleado` (
  `empleado_id` int NOT NULL,
  `nombre` varchar(25) NOT NULL,
  `apellido1` varchar(25) NOT NULL,
  `apellido2` varchar(25) DEFAULT NULL,
  `telefono` varchar(12) NOT NULL,
  `fecha_de_nacimiento` date NOT NULL,
  `sueldo` double NOT NULL,
  `entrada` time NOT NULL,
  `salida` time NOT NULL,
  `departamento_id` int NOT NULL,
  `supervisor_id` int NOT NULL,
  PRIMARY KEY (`empleado_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `empleado`
--

LOCK TABLES `empleado` WRITE;
/*!40000 ALTER TABLE `empleado` DISABLE KEYS */;
INSERT INTO `empleado` VALUES (1,'Juan','Escutia','Barrios','6646789043','1983-12-25',15000,'08:00:00','16:00:00',1,1),(2,'Jesus','Escoboza','Hernandez','6641237840','1978-10-13',8000,'08:00:00','13:00:00',4,3),(3,'Alberto','Jacques','Juarez','6640568491','1975-02-15',15000,'08:00:00','16:00:00',4,3),(4,'Jose','Ramirez','Cervantes','6649464923','1967-06-01',7000,'08:00:00','13:00:00',3,5),(5,'Antonio','Sanchez','Rodriguez','6640192032','1971-08-09',15000,'08:00:00','16:00:00',3,5),(6,'Roberto','Hernandez','Suarez','6649457981','1983-11-05',10000,'08:00:00','16:00:00',2,10),(7,'Trinidad','Lopez','Rosas','6635987400','1985-02-26',8000,'08:00:00','13:00:00',2,10),(8,'Alejandro','Garcia','Robles','6646908795','1973-07-12',15000,'08:00:00','16:00:00',5,8),(9,'Joaquin','Rodriguez','Saavedra','6641326586','1988-10-11',8000,'13:00:00','18:00:00',1,1),(10,'Luis','Garcia','Postigo','6649875709','1979-04-15',15000,'08:00:00','16:00:00',2,10),(11,'Pavel','Pardo','Lopez','6646577577','1982-09-10',8000,'08:00:00','13:00:00',5,8),(12,'Duilio ','Davino','Ortiz','6641679859','1990-08-20',8000,'13:00:00','18:00:00',5,8),(13,'Edson','Alvarez','Suarez','6649564965','1983-05-10',8000,'08:00:00','13:00:00',1,1),(14,'Guido','Rodriguez','Messi','6646566976','1970-11-09',8000,'13:00:00','18:00:00',4,3),(15,'Javier','Alarcon','Bermudez','6645870965','1969-12-21',10000,'08:00:00','16:00:00',4,3);
/*!40000 ALTER TABLE `empleado` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `equipo`
--

DROP TABLE IF EXISTS `equipo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `equipo` (
  `equipo_id` int NOT NULL,
  `departamento_id` int NOT NULL,
  `descripcion` varchar(45) NOT NULL,
  `status` varchar(15) NOT NULL,
  PRIMARY KEY (`equipo_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `equipo`
--

LOCK TABLES `equipo` WRITE;
/*!40000 ALTER TABLE `equipo` DISABLE KEYS */;
INSERT INTO `equipo` VALUES (1,1,'bote de pintura','bueno'),(2,1,'gato hidraulico','bueno'),(3,1,'soldadora','dañado'),(4,2,'voltimetro','bueno'),(5,2,'gato hidraulico','bueno'),(6,2,'destornillador','dañado'),(7,3,'gato hidraulico','bueno'),(8,3,'desmontador','bueno'),(9,3,'balanceadora','bueno'),(10,4,'gato hidraulico','bueno'),(11,4,'pinzas','dañado'),(12,4,'juego de llaves','bueno'),(13,5,'manometro','bueno'),(14,5,'torquimetro','bueno'),(15,5,'atornillador','bueno');
/*!40000 ALTER TABLE `equipo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `inventario`
--

DROP TABLE IF EXISTS `inventario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `inventario` (
  `inventario_id` int NOT NULL,
  `descripcion` varchar(45) NOT NULL,
  `costo` double NOT NULL,
  `stock` int NOT NULL,
  `min_stock` int NOT NULL,
  `max_stock` int NOT NULL,
  PRIMARY KEY (`inventario_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inventario`
--

LOCK TABLES `inventario` WRITE;
/*!40000 ALTER TABLE `inventario` DISABLE KEYS */;
INSERT INTO `inventario` VALUES (1,'wiper',100,50,10,100),(2,'aromatizante',50,100,25,200),(3,'espejo retrovisor',450,45,5,50),(4,'pinzas',250,70,10,100),(5,'juego de tapetes',300,100,25,250),(6,'amortiguador',1000,60,20,80),(7,'calavera',250,100,20,150),(8,'disco de freno',500,30,10,50),(9,'faro',400,50,20,80),(10,'kit de leds',450,35,10,40),(11,'transformador',300,15,5,50),(12,'llanta 175/70',1000,35,5,40),(13,'llanta 185/70',1100,30,5,40),(14,'filtro de aceite',100,25,10,50),(15,'anticongelante 1l.',300,60,10,100);
/*!40000 ALTER TABLE `inventario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `promocion`
--

DROP TABLE IF EXISTS `promocion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `promocion` (
  `promocion_id` int NOT NULL,
  `descripcion` varchar(45) NOT NULL,
  `porcentaje` double NOT NULL,
  `activo` tinyint(1) NOT NULL,
  PRIMARY KEY (`promocion_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `promocion`
--

LOCK TABLES `promocion` WRITE;
/*!40000 ALTER TABLE `promocion` DISABLE KEYS */;
INSERT INTO `promocion` VALUES (1,'50% descuento en pintura',0.5,0),(2,'25% descuento en cambio de llantas',0.25,0),(3,'10% descuento en  refacciones',0.1,1),(4,'30% descuento en cambio de bateria ',0.3,1),(5,'20% descuento en carroceria',0.2,0),(6,'15% descuento en transmision',0.15,1),(7,'5% descuento en cambio de aceite',0.5,0),(8,'8% descuento en cambio de balatas',0.8,1),(9,'25% descuento en daños carroceria',0.25,0),(10,'20% descuento en suspension',0.2,0),(11,'12% descuento en aromatizantes',0.12,1),(12,'5% descuento en amortiguadores',0.5,1),(13,'15% descuento en arreglo de defensas',0.15,1),(14,'10% descuento en alineacion',0.1,1),(15,'25% descuento en motores',0.25,1),(16,'0% descuento en aire acondicionado',0.1,1),(17,'15% descuento en balanceo',0.15,0),(18,'20% descuento en electrico',0.2,1),(19,'15% descuento mecanica general',0.15,1);
/*!40000 ALTER TABLE `promocion` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `servicio`
--

DROP TABLE IF EXISTS `servicio`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `servicio` (
  `servicio_id` int NOT NULL,
  `nombre` varchar(25) NOT NULL,
  `descripcion` varchar(45) NOT NULL,
  `costo` double NOT NULL,
  `tiempo` int NOT NULL,
  `venta_id` int NOT NULL,
  `departamento_id` int NOT NULL,
  `empleado_id` varchar(45) NOT NULL,
  `garantia` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`servicio_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `servicio`
--

LOCK TABLES `servicio` WRITE;
/*!40000 ALTER TABLE `servicio` DISABLE KEYS */;
INSERT INTO `servicio` VALUES (1,'Aceite','Cambio de aceite',600,30,1,4,'2',0),(2,'Transmision','Reparacion de transmision',5000,180,2,4,'3',0),(3,'Motor','Reparacion de motor',4000,150,3,4,'14',0),(4,'Inyectores','Cambio de inyectores',600,40,4,4,'3',0),(5,'Direccion','Reparacion de direccion',600,40,5,4,'15',0),(6,'Bolsa de aire','Cambio de bolsa de aire',400,50,6,1,'9',0),(7,'Amortiguadores','Cambio de amortiguadores',600,50,7,4,'2',0),(8,'Aire acondicionado','Reparacion de aire',400,40,8,4,'14',0),(9,'Suspension','Reparacion de suspension',500,50,9,3,'4',1),(10,'Alineacion','Reparacion de alineacion',500,50,10,3,'5',1),(11,'Balanceo','Reparacion de balanceo',500,60,11,3,'5',1),(12,'Baterias','Cambio de bateria',600,30,12,2,'6',1),(13,'Electrico en general','Reparacion electrica',600,60,13,2,'7',0),(14,'Mecanica general','Reparacion en general',350,60,14,4,'2',0),(15,'Viaje','Revision de viaje',300,30,15,4,'2',0),(16,'Llantas','Cambio de llantas',1200,60,16,4,'14',1),(17,'Aceite','Venta de aceite',350,30,17,5,'11',0),(18,'Pintura','Cambio de pintura de carro',1500,240,18,1,'9',0),(19,'Diagnostico','Chequeo inicial de carro',200,30,19,4,'3',0),(20,'Suspension','Reparacion de suspension',500,50,19,4,'3',1),(21,'Diagnostico','Chequeo inicial de carro',200,30,20,4,'6',0);
/*!40000 ALTER TABLE `servicio` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `taller`
--

DROP TABLE IF EXISTS `taller`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `taller` (
  `taller_id` int NOT NULL AUTO_INCREMENT,
  `direccion` varchar(125) NOT NULL,
  `telefono` varchar(14) NOT NULL,
  PRIMARY KEY (`taller_id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `taller`
--

LOCK TABLES `taller` WRITE;
/*!40000 ALTER TABLE `taller` DISABLE KEYS */;
INSERT INTO `taller` VALUES (1,'Calle fantasma 123','6644543214'),(2,'Campo caligine','6641233212'),(3,'Calle Lopez Mateos 9874','6650987890'),(4,'Avenida Revolucion 3452','6634561243'),(5,'Calle Santa Fe 9878','6644366578'),(6,'Calle Santa Anna 5381','6641234567'),(7,'Colonia Lomas Taurinas 3891','6640987654'),(8,'Calle Benito Juarez 6122','6644325176'),(9,'Avenida Emiliano Zapata 3333','6631335846'),(10,'Calle Teniente Guerrero','6645123400'),(11,'Calle Plutarco Elias Calles 1122','6641029385'),(12,'Avenida Constitucion 0001','6646029582'),(13,'Calle Coahuila 5918','6645638826'),(14,'Avenida Buenos Aires 6899','6645214239'),(15,'Avenida Buena Vista 9292','6649639229');
/*!40000 ALTER TABLE `taller` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ventas`
--

DROP TABLE IF EXISTS `ventas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ventas` (
  `venta_id` int NOT NULL,
  `fecha` date NOT NULL,
  `taller_id` int NOT NULL,
  `cliente_id` int NOT NULL,
  `carro_id` int NOT NULL,
  `cajero_id` int NOT NULL,
  `promocion_id` int DEFAULT NULL,
  `total` double NOT NULL,
  PRIMARY KEY (`venta_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ventas`
--

LOCK TABLES `ventas` WRITE;
/*!40000 ALTER TABLE `ventas` DISABLE KEYS */;
INSERT INTO `ventas` VALUES (1,'2020-03-15',2,1,1,2,NULL,600),(2,'2020-03-16',1,2,2,2,6,4250),(3,'2020-03-16',5,3,3,2,15,3000),(4,'2020-03-16',3,4,4,5,NULL,600),(5,'2020-02-17',4,5,5,5,14,540),(6,'2020-01-17',1,6,6,1,NULL,400),(7,'2020-02-21',4,7,7,3,12,570),(8,'2020-02-21',3,8,8,3,16,360),(9,'2020-03-23',5,9,9,2,NULL,500),(10,'2020-03-23',2,10,10,2,14,450),(11,'2020-03-27',4,11,11,1,17,500),(12,'2020-04-10',1,12,12,5,4,420),(13,'2020-04-12',5,13,13,1,18,480),(14,'2020-04-15',2,14,14,1,NULL,350),(15,'2020-04-16',3,15,15,5,19,255),(16,'2020-04-19',4,16,16,5,2,1200),(17,'2020-04-20',1,2,2,3,7,350),(18,'2020-04-20',1,2,6,2,1,1500),(19,'2020-04-21',3,3,3,1,NULL,700),(20,'2020-04-15',2,3,3,2,NULL,200);
/*!40000 ALTER TABLE `ventas` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-26 15:11:36
