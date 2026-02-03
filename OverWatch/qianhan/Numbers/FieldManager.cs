namespace InfinityMemoriesEngine.OverWatch.qianhan.Numbers
{
    /// <summary>
    /// 泛类型通用字符段管理器，建议在轻量场景下使用，如配置字段、运行状态标记等
    /// 若用在复杂逻辑或需要状态追踪下使用，请使用<see cref="StringFieldManager"/>或数据管理器<see cref="DataManager"/>
    /// </summary>
    /// <typeparam name="R"></typeparam>
    public static class FieldManager<R>
    {
        private static Dictionary<string, R> fields = new Dictionary<string, R>();

        public static void set(string key, R value) => fields[key] = value;
        public static R? get(string key) => fields.TryGetValue(key, out var val) ? val : default;
        public static bool Has(string key) => fields.ContainsKey(key);
        public static void Remove(string key) => fields.Remove(key);
        public static void Clear() => fields.Clear();
    }
}
