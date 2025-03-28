using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.DAL.Models
{
    public class ClassRegistration
    {
        public int Id { get; set; }

        public DateTime RegistrationDate { get; set; }

        // Статус реєстрації (підтверджено, скасовано, в очікуванні)
        [StringLength(20)]
        public string Status { get; set; }

        public int ClientId { get; set; }

        public int ClassScheduleId { get; set; }

        // Зв'язки (1:M)
        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        [ForeignKey("ClassScheduleId")]
        public ClassSchedule ClassSchedule { get; set; }

        // Зв'язок з візитом (1:1)
        public Visit Visit { get; set; }
    }
} 