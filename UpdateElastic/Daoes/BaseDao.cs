namespace UpdateElastic.Daoes
{
    public abstract class BaseDao<T>
    {
        public abstract T get(string id);
        public abstract bool insert(string id, T value);
        public abstract bool delete(string id);
        public abstract bool update(string id, T value);
    }
}
