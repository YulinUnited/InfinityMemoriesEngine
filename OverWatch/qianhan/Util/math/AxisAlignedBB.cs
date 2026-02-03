using InfinityMemoriesEngine.OverWatch.qianhan.blocks;
using InfinityMemoriesEngine.OverWatch.qianhan.Numbers;
using static InfinityMemoriesEngine.OverWatch.qianhan.Numbers.Mathf;

namespace InfinityMemoriesEngine.OverWatch.qianhan.Util.math
{
    public class AxisAlignedBB
    {
        public double minX;
        public double minY;
        public double minZ;
        public double maxX;
        public double maxY;
        public double maxZ;

        public AxisAlignedBB(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            this.minX = Mathf.Mathf_Min(x1, x2);
            this.minY = Mathf.Mathf_Min(y1, y2);
            this.minZ = Mathf.Mathf_Min(z1, z2);
            this.maxX = Mathf.Mathf_Max(x1, x2);
            this.maxY = Mathf.Mathf_Max(y1, y2);
            this.maxZ = Mathf.Mathf_Max(z1, z2);
        }
        public AxisAlignedBB(Block pos)
        {
            pos.getX();
            pos.getY();
            pos.getZ();
        }
        public AxisAlignedBB(Vector3 min, Vector3 max)
        {
            minX = min.x;
            minY = min.y;
            minZ = min.z;
            maxX = max.x;
            maxY = max.y;
            maxZ = max.z;
        }
        public AxisAlignedBB setMaxY(double y2)
        {
            return new AxisAlignedBB(this.minX, this.minY, this.minZ, this.maxX, y2, this.maxZ);
        }
        public override bool Equals(object? obj)
        {
            if (this == obj)
            {
                return true;
            }
            else if (!(obj is AxisAlignedBB))
            {
                return false;
            }
            else
            {
                AxisAlignedBB axisAlignedBB = (AxisAlignedBB)obj;
                return this.minX == axisAlignedBB.minX && this.minY == axisAlignedBB.minY && this.minZ == axisAlignedBB.minZ && this.maxX == axisAlignedBB.maxX && this.maxY == axisAlignedBB.maxY && this.maxZ == axisAlignedBB.maxZ;
            }
        }

        public AxisAlignedBB grow(double x, double y, double z)
        {
            double d0 = this.minX - x;
            double d1 = this.minY - y;
            double d2 = this.minZ - z;
            double d3 = this.maxX + x;
            double d4 = this.maxY + y;
            double d5 = this.maxZ + z;
            return new AxisAlignedBB(d0, d1, d2, d3, d4, d5);
        }
        public AxisAlignedBB grow(double value)
        {
            return this.grow(value, value, value);
        }

        public AxisAlignedBB intersect(AxisAlignedBB other)
        {
            double d0 = Mathf.Mathf_Max(this.minX, other.minX);
            double d1 = Mathf.Mathf_Max(this.minY, other.minY);
            double d2 = Mathf.Mathf_Max(this.minZ, other.minZ);
            double d3 = Mathf.Mathf_Min(this.maxX, other.maxX);
            double d4 = Mathf.Mathf_Min(this.maxY, other.maxY);
            double d5 = Mathf.Mathf_Min(this.maxZ, other.maxZ);
            return new AxisAlignedBB(d0, d1, d2, d3, d4, d5);
        }

        public AxisAlignedBB union(AxisAlignedBB other)
        {
            double d0 = Mathf.Mathf_Min(this.minX, other.minX);
            double d1 = Mathf.Mathf_Min(this.minY, other.minY);
            double d2 = Mathf.Mathf_Min(this.minZ, other.minZ);
            double d3 = Mathf.Mathf_Max(this.maxX, other.maxX);
            double d4 = Mathf.Mathf_Max(this.maxY, other.maxY);
            double d5 = Mathf.Mathf_Max(this.maxZ, other.maxZ);
            return new AxisAlignedBB(d0, d1, d2, d3, d4, d5);
        }

        public AxisAlignedBB offset(double x, double y, double z)
        {
            return new AxisAlignedBB(this.minX + x, this.minY + y, this.minZ + z, this.maxX + x, this.maxY + y, this.maxZ + z);
        }
        public AxisAlignedBB offset(Block pos)
        {
            return new AxisAlignedBB(this.minX + (double)pos.getX(), this.minY + (double)pos.getY(), this.minZ + (double)pos.getZ(), this.maxX + (double)pos.getX(), this.maxY + (double)pos.getY(), this.maxZ + (double)pos.getZ());
        }
        public AxisAlignedBB offset(Vector3 vec)
        {
            return this.offset((double)vec.x, (double)vec.y, (double)vec.z);
        }
        public double calculateXOffset(AxisAlignedBB other, double offsetX)
        {
            if (other.maxY > this.minY && other.minY < this.maxY && other.maxZ > this.minZ && other.minZ < this.maxZ)
            {
                if (offsetX > 0.0D && other.maxX <= this.minX)
                {
                    double d1 = this.minX - other.maxX;

                    if (d1 < offsetX)
                    {
                        offsetX = d1;
                    }
                }
                else if (offsetX < 0.0D && other.minX >= this.maxX)
                {
                    double d0 = this.maxX - other.minX;

                    if (d0 > offsetX)
                    {
                        offsetX = d0;
                    }
                }

                return offsetX;
            }
            else
            {
                return offsetX;
            }
        }

        public double calculateYOffset(AxisAlignedBB other, double offsetY)
        {
            if (other.maxX > this.minX && other.minX < this.maxX && other.maxZ > this.minZ && other.minZ < this.maxZ)
            {
                if (offsetY > 0.0D && other.maxY <= this.minY)
                {
                    double d1 = this.minY - other.maxY;

                    if (d1 < offsetY)
                    {
                        offsetY = d1;
                    }
                }
                else if (offsetY < 0.0D && other.minY >= this.maxY)
                {
                    double d0 = this.maxY - other.minY;

                    if (d0 > offsetY)
                    {
                        offsetY = d0;
                    }
                }

                return offsetY;
            }
            else
            {
                return offsetY;
            }
        }

        public double calculateZOffset(AxisAlignedBB other, double offsetZ)
        {
            if (other.maxX > this.minX && other.minX < this.maxX && other.maxY > this.minY && other.minY < this.maxY)
            {
                if (offsetZ > 0.0D && other.maxZ <= this.minZ)
                {
                    double d1 = this.minZ - other.maxZ;

                    if (d1 < offsetZ)
                    {
                        offsetZ = d1;
                    }
                }
                else if (offsetZ < 0.0D && other.minZ >= this.maxZ)
                {
                    double d0 = this.maxZ - other.minZ;

                    if (d0 > offsetZ)
                    {
                        offsetZ = d0;
                    }
                }

                return offsetZ;
            }
            else
            {
                return offsetZ;
            }
        }

        public bool intersects(AxisAlignedBB other)
        {
            return this.intersects(other.minX, other.minY, other.minZ, other.maxX, other.maxY, other.maxZ);
        }

        public bool intersects(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            return this.minX < x2 && this.maxX > x1 && this.minY < y2 && this.maxY > y1 && this.minZ < z2 && this.maxZ > z1;
        }

        public bool intersects(Vector3 min, Vector3 max)
        {
            return this.intersects(Mathf.Mathf_Min(min.x, max.x), Mathf.Mathf_Min(min.y, max.y), Mathf.Mathf_Min(min.z, max.z), Mathf.Mathf_Max(min.x, max.x), Mathf.Mathf_Max(min.y, max.y), Mathf.Mathf_Max(min.z, max.z));
        }

        public bool contains(Vector3 vec)
        {
            if (vec.x > this.minX && vec.x < this.maxX)
            {
                if (vec.y > this.minY && vec.y < this.maxY)
                {
                    return vec.z > this.minZ && vec.z < this.maxZ;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public double getAverageEdgeLength()
        {
            double d0 = this.maxX - this.minX;
            double d1 = this.maxY - this.minY;
            double d2 = this.maxZ - this.minZ;
            return (d0 + d1 + d2) / 3.0D;
        }
        public AxisAlignedBB shrink(double value)
        {
            return this.grow(-value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(minX, minY, minZ, maxX, maxY, maxZ);
        }
    }
}
