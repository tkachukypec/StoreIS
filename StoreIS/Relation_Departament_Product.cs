//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StoreIS
{
    using System;
    using System.Collections.Generic;
    
    public partial class Relation_Departament_Product
    {
        public int DepartamentId { get; set; }
        public int ProductId { get; set; }
        public Nullable<int> Count { get; set; }
    
        public virtual Departament Departament { get; set; }
        public virtual Product Product { get; set; }
    }
}
