using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Mapping
{
	public class AutoMappings
	{
		private MapperConfiguration _configuration;

		#region singleton pattern
		private static readonly Lazy<AutoMappings> lazy = new Lazy<AutoMappings>(() => new AutoMappings());

		public static AutoMappings Current { get { return lazy.Value; } }

		private AutoMappings()
		{ }
		#endregion

		public void RegisterMapping(IEnumerable<Type> executingAssemblyTypes)
		{
			LoadMappings(executingAssemblyTypes);
		}

		/// <summary>
		/// Creates all mappings for classes implementing IHaveCustomMappings
		/// </summary>
		/// <param name="types">Types loaded from assembly</param>
		private void LoadMappings(IEnumerable<Type> types)
		{
			var customMaps = (from t in types
							  from i in t.GetInterfaces()
							  where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
									!t.IsAbstract &&
									!t.IsInterface
							  select (IHaveCustomMappings)Activator.CreateInstance(t)).ToArray();

			_configuration = new MapperConfiguration(config =>
			{
				foreach (var map in customMaps)
				{
					map.CreateMappings(config);
				}

			});
		}

		public MapperConfiguration Configuration
		{
			get
			{
				return _configuration;
			}
		}

		public IMapper Mapper
		{
			get
			{
				return _configuration.CreateMapper();
			}
		}
	}
}
