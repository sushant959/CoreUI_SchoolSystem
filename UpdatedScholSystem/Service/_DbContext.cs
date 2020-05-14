using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UpdatedScholSystem.Models;

namespace UpdatedScholSystem.Service
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class UpdatedSchoolSystemDbContext:DbContext
    {
        public UpdatedSchoolSystemDbContext()
            : base("name=ebMasterDb_MySqlConnection") { }

        public DbSet<UserControl> userControls { get; set; }
    }
}