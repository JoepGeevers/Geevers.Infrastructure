namespace Geevers.Infrastructure
{
    using System;

	public struct Dirty<T>
	{
		public bool IsDirty { get; private set; }

		private T value;

		public Dirty(T value)
		{
			this.value = value;
			this.IsDirty = true;
		}

		public static implicit operator T(Dirty<T> dirty) => dirty.value;
		public static implicit operator Dirty<T>(T value) => new Dirty<T>(value);

		public void IfDirty(Action<T> action)
		{
			if (this.IsDirty)
			{
				action(this.value);
			}
		}
	}
}