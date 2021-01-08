using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Ecommerce.Models;

namespace Ecommerce.Areas.Admin.Models
{
    // lớp giúp lọc ra những người dùng theo tên Role
    // và những người dùng chưa có tên role được chọn
    public class RoleEdit
    {
        // lấy thông tin role
        public IdentityRole Role { get; set; }

        // ds người dùng có role được chọn
        public IEnumerable<AppUser> Member { get; set; }

        // ds người dùng không có role được chọn
        public IEnumerable<AppUser> NonMember { get; set; }
    }
}