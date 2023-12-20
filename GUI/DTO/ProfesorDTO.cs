﻿using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class ProfesorDTO : INotifyPropertyChanged
    {
        public int ProfesorId {  get; set; }
        private string ime {  get; set; }
        public string Ime
        {
            get
            {
                return ime;
            }
            set
            {
                if(ime != value)
                {
                    ime = value;
                    OnPropertyChanged("Ime");
                }
            }
        
        }
        private string prezime {  get; set; }
        public string Prezime
        {
            get
            {
                return prezime;
            }
            set
            {
                if(prezime != value)
                {
                    prezime = value;
                    OnPropertyChanged("Prezime");
                }

            }
        }
        private DateTime datumRodjenja { get; set; }
        public DateTime DatumRodjenja
        {
            get
            {
                return datumRodjenja;
            }
            set
            {
                if (datumRodjenja != value)
                {
                    datumRodjenja = value;
                    OnPropertyChanged("DatumRodjenja");
                }
            }
        }
        private int adresaId {  get; set; }
        public int AdresaId
        {
            get
            {
                return adresaId;
            }
            set
            {
                if(adresaId != value)
                {
                    adresaId = value;
                    OnPropertyChanged("AdresaId");
                }
            }
        }
        private string kontaktTelefon {  get; set; }
        public string KontaktTelefon
        {
            get
            {
                return kontaktTelefon;
            }
            set
            {
                if(kontaktTelefon != value)
                {
                    kontaktTelefon = value;
                    OnPropertyChanged("KontaktTelefon");
                }
            }
        }
        private string email {  get; set; }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if(email != value)
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        private int brojLicneKarte {  get; set; }
        public int BrojLicneKarte
        {
            get
            {
                return brojLicneKarte;
            }
            set
            {
                if(brojLicneKarte != value)
                {
                    brojLicneKarte = value;
                    OnPropertyChanged("BrojLicneKarte");
                }
            }
        }
        private string zvanje {  get; set; }
        public string Zvanje
        {
            get
            {
                return zvanje;
            }
            set
            {
                if(zvanje != value)
                {
                    zvanje = value;
                    OnPropertyChanged("Zvanje");
                }
            }



        }
        private int godineStaza {  get; set; }
        public int GodineStaza
        {
            get
            {
                return godineStaza;
            }
            set
            {
                if(godineStaza != value)
                {
                    godineStaza = value;
                    OnPropertyChanged("GodineStaza");
                }
            }
        }

        public Profesor toProfesor()
        {
            return new Profesor(ime, prezime, datumRodjenja, adresaId, kontaktTelefon, email, brojLicneKarte, zvanje, godineStaza);
        }

        public ProfesorDTO()
        {

        }


        public event PropertyChangedEventHandler? PropertyChanged;


        public ProfesorDTO(Profesor profesor)
        {
            ProfesorId = profesor.ProfesorId;
            ime = profesor.Ime;
            prezime = profesor.Prezime;
            datumRodjenja = profesor.DatumRodjenja;
            adresaId = profesor.AdresaId;
            kontaktTelefon = profesor.KontaktTelefon;
            email = profesor.Email;
            brojLicneKarte = profesor.BrojLicneKarte;
            zvanje = profesor.Zvanje;
            godineStaza = profesor.GodineStaza;


        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ProfesorDTO Clone()
        {
            return new ProfesorDTO
            {
                Ime = this.Ime,
                Prezime = this.Prezime,
                DatumRodjenja = this.DatumRodjenja,
                AdresaId = this.AdresaId,
                KontaktTelefon = this.KontaktTelefon,
                Email = this.Email,
                BrojLicneKarte = this.BrojLicneKarte,
                Zvanje = this.Zvanje,
                GodineStaza = this.GodineStaza,
            };
        }


    }
}












