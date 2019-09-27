using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;

namespace FizzBuzz.Framework.Autofac
{
	public abstract class AutofacModule : IAutofacModule
	{
		private readonly OnActivatedHandlerCollection _onActivatedRegistrations = new OnActivatedHandlerCollection();
		private readonly OnInitiatingHandlerCollection _onInitiatingRegistrations = new OnInitiatingHandlerCollection();
		private readonly OnPreparingHandlerCollection _onPreparingRegistrations = new OnPreparingHandlerCollection();

		public abstract void Load(ContainerBuilder builder);

		public OnActivatedHandlerCollection GetOnActivationHandlers()
		{
			return _onActivatedRegistrations;
		}

		public OnInitiatingHandlerCollection GetOnInitiatingHandlers()
		{
			return _onInitiatingRegistrations;
		}

		public OnPreparingHandlerCollection GetOnPreparingHandlers()
		{
			return _onPreparingRegistrations;
		}

		[Obsolete("Use RegisterOnInitiatingHandler.")]
		public void RegisterOnActivatedHandler<T>(Action<ActivatedEventArgs<T>> action)
		{
			if (!_onActivatedRegistrations.ContainsKey(typeof(T)))
			{
				_onActivatedRegistrations.Add(typeof(T), new List<Action<ActivatedEventArgs<object>>>());
			}

			_onActivatedRegistrations[typeof(T)].Add(args => action(new ActivatedEventArgs<T>(args.Context, args.Component, args.Parameters, (T)args.Instance)));
		}

		public void RegisterOnInitiatingHandler<T>(Action<ActivatingEventArgs<T>> action)
		{
			if (!_onInitiatingRegistrations.ContainsKey(typeof(T)))
			{
				_onInitiatingRegistrations.Add(typeof(T), new List<Action<ActivatingEventArgs<object>>>());
			}

			_onInitiatingRegistrations[typeof(T)].Add(args => action(new ActivatingEventArgs<T>(args.Context, args.Component, args.Parameters, (T)args.Instance)));
		}

		public void RegisterOnPreparingHandler<T>(Action<PreparingEventArgs> action)
		{
			if (!_onInitiatingRegistrations.ContainsKey(typeof(T)))
			{
				_onPreparingRegistrations.Add(typeof(T), new List<Action<PreparingEventArgs>>());
			}

			_onPreparingRegistrations[typeof(T)].Add(args => action(new PreparingEventArgs(args.Context, args.Component, args.Parameters)));
		}
	}
}
