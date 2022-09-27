﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innstant.Models
{
	public class InnstantHotels
	{
		public int HotelId { get; set; }
		public string HotelName { get; set; }
		public string Address { get; set; }
		public int Status { get; set; }
		public int Zip { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }
		//public int lat { get; set; }	// 44.072700500488
		//public int lon { get; set; }  // 12.576800346374
		public int Stars { get; set; }
		public string Seoname { get; set; }
	}
}