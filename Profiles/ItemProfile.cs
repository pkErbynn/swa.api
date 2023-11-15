using System;
using System.Runtime.InteropServices;
using AutoMapper;
using SWA.API.Dtos;
using SWA.API.Properties;

namespace SWA.API.Profiles
{
	public class ItemProfile: Profile
    {
        public ItemProfile()
		{
            CreateMap<Item, ItemReadDto>();
            CreateMap<ItemAddDto, Item>();
        }
	}
}

