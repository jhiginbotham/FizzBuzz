using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;

namespace FizzBuzz.Framework.Autofac
{
	public abstract class AutofacContainer : IIocContainer
	{
		private bool _isBuilt = false;
		private readonly Lazy<ContainerBuilder> _builder;
		protected readonly Lazy<IContainer> Container;

		protected abstract IEnumerable<ISimpleAutofacModule> Modules { get; }

		public ContainerBuilder Builder => _builder.Value;

		protected AutofacContainer()
		{
			_builder = new Lazy<ContainerBuilder>(Load);
			Container = new Lazy<IContainer>(Build);
		}

		public bool TryResolve<T>(out T type)
		{
			return Container.Value.TryResolve(out type);
		}

		public T Resolve<T>()
		{
			return Container.Value.Resolve<T>();
		}

		public virtual IIocScope BeginScope()
		{
			return new AutofacScope(Container.Value.BeginLifetimeScope());
		}

		public virtual IIocScope BeginScope(string name)
		{
			return new AutofacScope(Container.Value.BeginLifetimeScope(name));
		}

		public IContainer Build()
		{
			if (_isBuilt)
			{
				throw new InvalidOperationException("Container is already built. Cannot rebuild an already built container.");
			}

			var container = Builder.Build();
			_isBuilt = true;
			return container;
		}

		#region Load Methods

		private ContainerBuilder Load()
		{
			var builder = new ContainerBuilder();
			Load(builder);
			return builder;
		}

		public void Load(ContainerBuilder builder)
		{
			var distinctTypes = new HashSet<Type>();
			var onActivatedHandlers = new OnActivatedHandlerCollection();
			var onInitiatingHandlers = new OnInitiatingHandlerCollection();
			var onPreparingHandlers = new OnPreparingHandlerCollection();

			foreach (var mod in GetNestedModules(this))
			{
				if (!distinctTypes.Contains(mod.GetType()))
				{
					distinctTypes.Add(mod.GetType());
					mod.Load(builder);

					if (mod is IAutofacModule autofacModule)
					{
						onActivatedHandlers.AddRegistration(autofacModule.GetOnActivationHandlers());
						onInitiatingHandlers.AddRegistration(autofacModule.GetOnInitiatingHandlers());
						onPreparingHandlers.AddRegistration(autofacModule.GetOnPreparingHandlers());
					}
				}
			}

			if (onActivatedHandlers.Any())
			{
				builder.RegisterModule(new MultiEventRegistrationModule(onActivatedHandlers));
			}

			if (onInitiatingHandlers.Any())
			{
				builder.RegisterModule(new MultiEventRegistrationModule(onInitiatingHandlers));
			}

			if (onPreparingHandlers.Any())
			{
				builder.RegisterModule(new MultiEventRegistrationModule(onPreparingHandlers));
			}
		}

		private List<ISimpleAutofacModule> GetNestedModules(AutofacContainer container)
		{
			var totalModules = new List<ISimpleAutofacModule>();

			foreach (var mod in container.Modules)
			{
				if (mod is AutofacContainer modContainer)
				{
					totalModules.AddRange(GetNestedModules(modContainer));
				}
				else
				{
					totalModules.Add(mod);
				}
			}

			return totalModules;
		}

		#endregion

		public void Dispose()
		{
			if (Container.IsValueCreated)
			{
				Container.Value.Dispose();
			}
		}

	}
}
