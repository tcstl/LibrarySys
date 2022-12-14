//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibrarySys.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            this.ReaderBorrows = new HashSet<ReaderBorrow>();
        }
    
        public int ID_book { get; set; }
        [Required(ErrorMessage = "???? ???????? ?????????? ?? ???????")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = " ???? ?? ? ?? 30 ???????")]
        public string Title { get; set; }
        [Required(ErrorMessage = "???? ???????? ????")]
        public Nullable<int> ID_genre { get; set; }
        [Required(ErrorMessage = "???? ???????? ?????")]
        public Nullable<int> ID_author { get; set; }
        [Required(ErrorMessage = "???? ???????? ???????? ?? ??????????. ?????????? ?? 4 ???????")]
        [StringLength(4, MinimumLength = 1, ErrorMessage = " ???? ?? ? ?? 4 ???????")]
        public string Year { get; set; }
        public Nullable<int> ID_publisher { get; set; }
        [Required(ErrorMessage = "???? ???????? ???? ????????. ?????????? ?? 4 ???????")]
        [StringLength(4, MinimumLength = 1, ErrorMessage = " ???? ?? ? ?? 4 ???????")]
        public string Pages { get; set; }
        public Nullable<bool> Available { get; set; }
        [Required(ErrorMessage = "???? ???????? ????.")]
        [StringLength(4, MinimumLength = 1, ErrorMessage = " ???? ?? ? ?? 4 ???????")]
        public Nullable<int> Stock { get; set; }
        public virtual Author Author { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Publisher Publisher { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReaderBorrow> ReaderBorrows { get; set; }
    }
}
