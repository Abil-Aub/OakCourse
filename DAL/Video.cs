//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Video
    {
        public int ID { get; set; }
        public string VideaPath { get; set; }
        public string Ttitle { get; set; }
        public string OriginalVideoPath { get; set; }
        public System.DateTime AddDate { get; set; }
        public int AddUserID { get; set; }
        public bool isDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public int LastUpdateUserID { get; set; }
        public System.DateTime LastUpdateDate { get; set; }
    
        public virtual T_User T_User { get; set; }
    }
}
