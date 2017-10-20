using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Pharmacy.Droid
{
    public class Product
    {
        public static List<Product> LoadProducts()
        {
            List<Product> result = new List<Product>();

            result.Add(new Product("7501125197437", "ACICLOVIR 250 MG FA C/5", "PFIZER", 530));
            result.Add(new Product("7501390912551", "CHOLAL MOD S-ALIME AMP10X5ML 431", "ITALMEX", 350));
            result.Add(new Product("7501082212143", "CIALIS 20 MG TAB 1 002", "LILLY", 240));
            result.Add(new Product("7501009071099", "PULMOZYME 2.5MG AMPS6X2.5ML", "ROCHE", 120));
            result.Add(new Product("7501092720911", "EMPLAY RIOPAN GEL 250ML 15 TWPKB Y", "TAKEDA DE MEXICO", 450));

            return result;
        }

        public Product(string codigo, string descripcion, string laboratorio, double precio)
        {
            this.Codigo = codigo;
            this.Descripcion = descripcion;
            this.Laboratorio = laboratorio;
            this.Precio = precio;
        }

        public string Codigo
        {
            get;
            set;
        }

        public string Descripcion
        {
            get;
            set;
        }

        public string Laboratorio
        {
            get;
            set;
        }

        public double Precio
        {
            get;
            set;
        }
    }
}