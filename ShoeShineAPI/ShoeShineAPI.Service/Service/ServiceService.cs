﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShoeShineAPI.Core.IRepositories;
using ShoeShineAPI.Core.Model;
using ShoeShineAPI.Service.Inheritance_Class;
using ShoeShineAPI.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShineAPI.Service.Service;

public class ServiceService : CommonAbstract<Core.Model.Service>, IServiceService
{
	IUnitRepository _unit;

	public ServiceService(IUnitRepository unit)
	{
		_unit = unit;
	}
	public async Task<IEnumerable<Core.Model.Service>> GetServicesByStoreId(IEnumerable<ServiceStore> serviceStores, int storeId)
	{
		// lấy những serviceId có trong list serviceStores bằng storeId truyển vào
		var matchingServiceId = _unit.ServiceStoreRepository.GetServiceIdByStoreId(storeId);
		// sau khi có list service Id lọc ra bởi storeId, ta lấy ra list service 
		var services = await GetAllDataAsync();
		var serviceOfStore = GetMatchingItems(matchingServiceId, services, x=> x.ServiceId);
		if (serviceOfStore != null)
			return serviceOfStore;
		else
			return Enumerable.Empty<Core.Model.Service>();
	}
	protected override async Task<IEnumerable<Core.Model.Service>> GetAllDataAsync()
	{
		return await _unit.ServiceRepository.GetAll();
	}

	public async Task<IEnumerable<Core.Model.Service>> GetServicesAsync()
	{
		var services = await GetAllDataAsync();
		return services;
	}
}
