using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityMemoriesEngine.OverWatch.qianhan.Log.lang;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Entities.xuechengai.game.nbt
{
    public class NBTTagList : NBTBase, IEnumerable<NBTBase>
    {
        //private static readonly Debug LOGGER = new Debug();
        //Logger Logger = new Debug();
        private List<NBTBase> tagList = new List<NBTBase>();
        private byte tagType = 0;

        public NBTTagList() { }

        public override void write(BinaryWriter output)
        {
            if (tagList.Count == 0)
            {
                tagType = 0;
            }
            else
            {
                tagType = tagList[0].getId();
            }

            output.Write(tagType);
            output.Write(tagList.Count);

            foreach (var tag in tagList)
            {
                tag.write(output);
            }
        }

        public override void read(BinaryReader input, int depth, NBTSizeTracker sizeTracker)
        {
            sizeTracker.read(296L);
            if (depth > 512)
            {
                throw new Exception("Tried to read NBT tag with too high complexity, depth > 512");
            }
            else
            {
                tagType = input.ReadByte();
                int count = input.ReadInt32();
                if (tagType == 0 && count > 0)
                {
                    throw new Exception("Missing type on ListTag");
                }
                else
                {
                    sizeTracker.read(32L * count);
                    tagList = new List<NBTBase>(count);

                    for (int i = 0; i < count; i++)
                    {
                        NBTBase nbtbase = createNewByType(tagType);
                        nbtbase.read(input, depth + 1, sizeTracker);
                        tagList.Add(nbtbase);
                    }
                }
            }
        }

        public override byte getId() => 9;

        public override string ToString()
        {
            return "[" + string.Join(",", tagList.Select(tag => tag.ToString())) + "]";
        }

        public void AppendTag(NBTBase nbt)
        {
            if (nbt.getId() == 0)
            {
                Debug.LogWarning("Invalid TagEnd added to ListTag");
            }
            else
            {
                if (tagType == 0)
                {
                    tagType = nbt.getId();
                }
                else if (tagType != nbt.getId())
                {
                    Debug.LogWarning("Adding mismatching tag types to tag list");
                    return;
                }

                tagList.Add(nbt);
            }
        }

        public void Set(int idx, NBTBase nbt)
        {
            if (nbt.getId() == 0)
            {
                Debug.LogWarning("Invalid TagEnd added to ListTag");
            }
            else if (idx >= 0 && idx < tagList.Count)
            {
                if (tagType == 0)
                {
                    tagType = nbt.getId();
                }
                else if (tagType != nbt.getId())
                {
                    Debug.LogWarning("Adding mismatching tag types to tag list");
                    return;
                }

                tagList[idx] = nbt;
            }
            else
            {
                Debug.LogWarning("Index out of bounds to set tag in tag list");
            }
        }

        public NBTBase RemoveTag(int i)
        {
            var tag = tagList[i];
            tagList.RemoveAt(i);
            return tag;
        }

        public bool HasNoTags() => tagList.Count == 0;

        public NBTTagCompound GetCompoundTagAt(int i)
        {
            if (i >= 0 && i < tagList.Count)
            {
                var nbt = tagList[i];
                if (nbt.getId() == 10)
                {
                    return (NBTTagCompound)nbt;
                }
            }

            return new NBTTagCompound();
        }

        public int GetIntAt(int index)
        {
            if (index >= 0 && index < tagList.Count)
            {
                var nbt = tagList[index];
                if (nbt.getId() == 3)
                {
                    return ((NBTTagInt)nbt).getInt();
                }
            }

            return 0;
        }

        public int[] GetIntArrayAt(int i)
        {
            if (i >= 0 && i < tagList.Count)
            {
                var nbt = tagList[i];
                if (nbt.getId() == 11)
                {
                    return ((NBTTagIntArray)nbt).GetIntArray();
                }
            }

            return new int[0];
        }

        public double GetDoubleAt(int i)
        {
            if (i >= 0 && i < tagList.Count)
            {
                var nbt = tagList[i];
                if (nbt.getId() == 6)
                {
                    return ((NBTTagDouble)nbt).getDouble();
                }
            }

            return 0.0;
        }

        public float GetFloatAt(int i)
        {
            if (i >= 0 && i < tagList.Count)
            {
                var nbt = tagList[i];
                if (nbt.getId() == 5)
                {
                    return ((NBTTagFloat)nbt).getFloat();
                }
            }

            return 0.0f;
        }

        public string GetStringTagAt(int i)
        {
            if (i >= 0 && i < tagList.Count)
            {
                var nbt = tagList[i];
                return nbt.getId() == 8 ? nbt.getString() : nbt.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public NBTBase Get(int idx)
        {
            return idx >= 0 && idx < tagList.Count ? tagList[idx] : new NBTTagEnd();
        }

        public int TagCount() => tagList.Count;

        public NBTTagList Copy()
        {
            var copy = new NBTTagList
            {
                tagType = tagType
            };

            foreach (var tag in tagList)
            {
                copy.tagList.Add(tag.copy());
            }

            return copy;
        }

        public override bool equals(object obj)
        {
            if (!base.Equals(obj))
            {
                return false;
            }

            if (obj is NBTTagList other)
            {
                return tagType == other.tagType && tagList.SequenceEqual(other.tagList);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ tagList.GetHashCode();
        }

        public int GetTagType() => tagType;

        public IEnumerator<NBTBase> GetEnumerator() => tagList.GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
