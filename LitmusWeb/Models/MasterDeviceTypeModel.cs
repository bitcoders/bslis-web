using System.Collections.Generic;

namespace LitmusWeb.Models
{
    public class MasterDeviceTypeModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MasterDeviceTypeModel()
        {
            this.RegisteredDevicesModel = new HashSet<RegisteredDevicesModel>();
        }

        public int Code { get; set; }
        public string DeviceType { get; set; }
        public string DeviceOS { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegisteredDevicesModel> RegisteredDevicesModel { get; set; }

    }
}