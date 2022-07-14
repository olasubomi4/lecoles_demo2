using System;
using System.ComponentModel.DataAnnotations;

namespace lecolesAI.Models
{
	public class courses
	{
		[Key]
		public int index { get; set; }
        public int course_id { get; set; }
        [Required]
		public string? course_title { get; set; }
        [Required]
        public string?  url { get; set; }

        public int price { get; set; }
        public int num_subscribers { get; set; }
        public int num_reviews { get; set; }
        public int num_lectures { get; set; }
        public string  level { get; set; }
        public float content_duration { get; set; }
        public string?  published_timestamp { get; set; }
        public string? subject { get; set; }
       
     
       
    }

}

