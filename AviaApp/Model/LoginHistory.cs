//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AviaApp.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class LoginHistory
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public System.DateTime LoginDateTime { get; set; }
        public Nullable<System.DateTime> LogoutDateTime { get; set; }
        public string UnsuccessgulLogoutReason { get; set; }
    
        public virtual Users Users { get; set; }
    }
}