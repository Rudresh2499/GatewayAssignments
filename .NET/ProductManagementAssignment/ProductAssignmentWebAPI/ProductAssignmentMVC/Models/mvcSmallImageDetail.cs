using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductAssignmentMVC.Models
{
    public partial class mvcSmallImageDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mvcSmallImageDetail()
        {
            this.mvcProductDetails = new HashSet<mvcProductDetail>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcProductDetail> mvcProductDetails { get; set; }
    }
}