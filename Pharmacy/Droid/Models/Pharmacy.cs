using System;
using System.Collections.Generic;

namespace Pharmacy.Droid
{
    public class Pharmacy
    {
        public static List<Pharmacy> LoadPharmacies()
        {
            List<Pharmacy> result = new List<Pharmacy>();

            result.Add(new Pharmacy("BOSQUES", "GRUPO DIAZ BARRIGA", "AV. STIM 1279", "Lomas del Chamizal", "Ciudad de México", "Cuajimalpa de Morelos", "Distrito Federal", "5129", 19.393202, -99.259986));
            result.Add(new Pharmacy("E+ Exhibimex", "E+ Exhibimex", "Calle 10 #132", "Col. San Pedro de los Pinos", "Distrito Federal", "Álvaro Obregón", "Distrito Federal", "1180", 19.389015, -99.193882));
            result.Add(new Pharmacy("E+ Miramontes", "E+ Miramontes", "Canal de Miramontes #2145", "Avante", "Delg. Alvaro Obregon", "Álvaro Obregón", "Distrito Federal", "4460", 19.195932, -9981366));
            result.Add(new Pharmacy("E+ Santa Fe", "E+ Santa Fe", "Av. Vasco de Quiroga #1816", "Santa Fé", "Álvaro Obregón", "Álvaro Obregón", "Distrito Federal", "1210", 19.3799, -99.24455));
            result.Add(new Pharmacy("FARMACIA PLATEROS", "FARMACIA PLATEROS", "AV CENTENARIO # 118 A", "MERCED GOMEZ", "ÁLVARO OBREGÓN", "ÁLVARO OBREGÓN", "CIUDAD DE MEXICO", "1600", 19.366245, -99.198415));
            result.Add(new Pharmacy("FARMAPRONTO JALALPA", "FARMAPRONTO JALALPA", "GUSTAVO DIAZ ORDAS 102", "JALALPA", "ÁLVARO OBREGÓN", "ÁLVARO OBREGÓN", "CIUDAD DE MEXICO", "1297", 19.378707, -99.230278));
            result.Add(new Pharmacy("FARMAPRONTO OBREGON", "FARMAPRONTO OBREGON", "PORTAL HIDALGO 4", "CENTRO", "ÁLVARO OBREGÓN", "ÁLVARO OBREGÓN", "MICHOACÁN DE OCAMPO", "58920", 19.826054, -101.038901));
            result.Add(new Pharmacy("FDM SAN JERONIMO", "FDM SAN JERONIMO", "Av. San Jerónimo no. 273, Locales. 14 y 17", "Pedregal De San Ángel", "Álvaro Obregón", "Álvaro Obregón", "Distrito Federal", "1090", 19.31265, -99.243947));
            result.Add(new Pharmacy("MEXICO", "GRUPO DIAZ BARRIGA", "SAGREDO 278", "Guadalupe Inn", "Ciudad de México", "Álvaro Obregón", "Distrito Federal", "1020", 19.360668, -99.189008));
            result.Add(new Pharmacy("MF CUAJIMALPA", "MULTI FARMACIAS", "AVENIDA VERACRUZ 93", "CUAJIMALPA", "CUAJIMALPA DE MORELOS", "CUAJIMALPA DE MORELOS", "CIUDAD DE MEXICO", "5000", 19.355755, -99.299929));
            result.Add(new Pharmacy("MODULO LOS ALPES", "MODULO LOS ALPES", "BARRANCA DEL MUERTO 490, COL. LOS ALPES 490", "LOS ALPES", "ÁLVARO OBREGÓN", "ÁLVARO OBREGÓN", "CIUDAD DE MEXICO", "72170", 19.059256, -98.249628));
            result.Add(new Pharmacy("MODULO PLAZA INN", "MODULO PLAZA INN", "AV. INSURGENTES SUR # 1863 PISO 5", "GUADALUPE INN", "ÁLVARO OBREGÓN", "ÁLVARO OBREGÓN", "CIUDAD DE MEXICO", "1020", 19.35424921, -99.18542795));
            result.Add(new Pharmacy("MULTIGENERICOS INSURGENTES", "GRUPO DIAZ BARRIGA", "AV. INSURGENTES 1657 INT 3", "GUADALUPE INN", "ÁLVARO OBREGÓN", "ÁLVARO OBREGÓN", "CIUDAD DE MEXICO", "1020", 19.362089, -99.182959));
            result.Add(new Pharmacy("SAN ANGEL-DF", "FARMACIAS FARMA VITA", "Boulevard Adolfo Lopez Mateos 2443", "ATLAMAYA", "ÁLVARO OBREGÓN", "ÁLVARO OBREGÓN", "CIUDAD DE MEXICO", "1760", 19.3482778, -99.2019166666666));
            result.Add(new Pharmacy("SANTA FE", "FARMACIAS FARMATLAN", "Prol. Paseo De La Reforma no. 19", "Paseo De Las Lomas", "Álvaro Obregón", "Álvaro Obregón", "Distrito Federal", "0", 19.371485, -99.265559));
            result.Add(new Pharmacy("SUCURSAL DE DIOS BARRANCA", "SUCURSAL DE DIOS BARRANCA", "Av. Revolución no. 1227, Local. 1b", "Los Alpes", "ÁLVARO OBREGÓN", "ÁLVARO OBREGÓN", "CIUDAD DE MEXICO", "1010", 19.361839, -99.189448));
            result.Add(new Pharmacy("SUCURSAL DE DIOS METROPOLITANO", "SUCURSAL DE DIOS METROPOLITANO", "Manzanillo no. 89, Local. 3", "Roma Sur", "ÁLVARO OBREGÓN", "ÁLVARO OBREGÓN", "CIUDAD DE MEXICO", "6760", 19.404834, -99.167016));
            result.Add(new Pharmacy("SUCURSAL DE DIOS UNIVERSIDAD", "SUCURSAL DE DIOS UNIVERSIDAD", "Av. Universidad no. 1469", "Florida", "ÁLVARO OBREGÓN", "ÁLVARO OBREGÓN", "CIUDAD DE MEXICO", "1030", 19.354563, -99.175455));
            result.Add(new Pharmacy("SUCURSAL DIOS CASTORENA", "SUCURSAL DIOS CASTORENA", "José María Castorena no. 320", "Cuajimapla", "CUAJIMALPA DE MORELOS", "CUAJIMALPA DE MORELOS", "CIUDAD DE MEXICO", "5000", 19.361449, -99.292787));
            result.Add(new Pharmacy("SUCURSAL GRAN PLAZA SAN FRANCISCO", "SUCURSAL GRAN PLAZA SAN FRANCISCO", "Calz. Desiertos De Los Leones no. 5525", "Alcantarilla", "Álvaro Obregón", "Álvaro Obregón", "Distrito Federal", "0", 19.33863286, -99.24252976));
            result.Add(new Pharmacy("SUCURSAL VYR SANTA FE", "SUCURSAL VYR SANTA FE", "Guillermo González Camarena no.999", "Centro De Cd. Santa Fe", "ÁLVARO OBREGÓN", "ÁLVARO OBREGÓN", "CIUDAD DE MEXICO", "1210", 19.371329, -99.263297));
            result.Add(new Pharmacy("SUCURSAL VYR SUE", "SUCURSAL VYR SUE", "Calz. De Las Águilas no. 1952", "Lomas De Axomiatla", "ÁLVARO OBREGÓN", "ÁLVARO OBREGÓN", "CIUDAD DE MEXICO", "1820", 19.341113, -99.252571));

            return result;
        }

        public Pharmacy(string nombreSucursal, string nombreComercial, string direccion, string colonia, string ciudad, string municipio, string estado, string codigoPostal, double latitud, double longitud)
        {
            this.NombreSucursal = nombreSucursal;
            this.NombreComercial = nombreComercial;
            this.Direccion = direccion;
            this.Colonia = colonia;
            this.Ciudad = ciudad;
            this.Municipio = municipio;
            this.Estado = estado;
            this.CodigoPostal = codigoPostal;
            this.Latitud = latitud;
            this.Longitud = longitud;
        }

        public string NombreSucursal
        {
            get;
            set;
        }

        public string NombreComercial
        {
            get;
            set;
        }

        public string Direccion
        {
            get;
            set;
        }

        public string Colonia
        {
            get;
            set;
        }

        public string Ciudad
        {
            get;
            set;
        }

        public string Municipio
        {
            get;
            set;
        }

        public string Estado
        {
            get;
            set;
        }

        public string CodigoPostal
        {
            get;
            set;
        }

        public double Latitud
        {
            get;
            set;
        }

        public double Longitud
        {
            get;
            set;
        }
    }
}