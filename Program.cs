namespace cap
{
    interface JsonValue {
        string print();
    }

    class JsonString : JsonValue {
        private string value;
        public string Value {
            get { return value; }
        }

        public JsonString(string value) {
            this.value = value;
        }

        public string print() {
            // Escape the string by replacing any backslashes with two backslashes and
            // any double quotes with a backslash and a double quote.
            var escaped = value.Replace("\\", "\\\\").Replace("\"", "\\\"");
            return "\"" + escaped + "\"";
        }
    }

    class JsonNumber : JsonValue {
        private double value;

        public JsonNumber(double value) {
            this.value = value;
        }

        public string print() {
            return value.ToString();
        }
    }

    class JsonObject : JsonValue {
        private Dictionary<string, JsonValue> values = new();
        public void add(string key, JsonValue value) {
            values[key] = value;
        }

        public string print() {
            string result = "{";
            foreach (var pair in values) {
                result += "\"" + pair.Key + "\":" + pair.Value.print() + ",";
            }
            // Remove the last comma (if any)
            if (values.Count > 0) {
                result = result.TrimEnd(',');
            }
            result += "}";
            return result;
        }
    }

    class JsonBoolean : JsonValue {
        private bool value;
        public JsonBoolean(bool value) {
            this.value = value;
        }

        public string print() {
            return value ? "true" : "false";
        }
    }

    class JsonNull : JsonValue {
        public string print() {
            return "null";
        }
    }

    class JsonArray : JsonValue {
        private List<JsonValue> values = new();
        public void add(JsonValue value) {
            this.values.Add(value);
        }
        public string print() {
            string result = "[";
            foreach (var value in values) {
                result += value.print() + ",";
            }
            // Remove the last comma (if any)
            if (values.Count > 0) {
                result = result.TrimEnd(',');
            }
            result += "]";
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            JsonObject obj = new JsonObject();
            obj.add("Name", new JsonString("Tim"));
            obj.add("Age", new JsonNumber(25));
            JsonObject address = new JsonObject();
            address.add("City", new JsonString("Seattle"));
            address.add("State", new JsonString("WA"));
            address.add("Zip", new JsonNumber(98101));
            address.add("Street", new JsonNull());
            obj.add("Address", address);
            obj.add("Active", new JsonBoolean(true));
            JsonArray array = new JsonArray();
            array.add(new JsonString("Apple"));
            array.add(new JsonNumber(10));
            array.add(new JsonBoolean(true));
            array.add(new JsonNull());
            obj.add("Array", array);
            string json = obj.print();
            Console.WriteLine(json);
        }
    }
}