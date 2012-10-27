namespace org.xiph.speex
{
	/// <summary>Vector Quantization.</summary>
	/// <remarks>Vector Quantization.</remarks>
	/// <author>Marc Gimpel, Wimba S.A. (mgimpel@horizonwimba.com)</author>
	/// <version>$Revision: 188 $</version>
	public class VQ
	{
		/// <summary>Finds the index of the entry in a codebook that best matches the input.</summary>
		/// <remarks>Finds the index of the entry in a codebook that best matches the input.</remarks>
		/// <param name="in">- the value to compare.</param>
		/// <param name="codebook">- the list of values to search through for the best match.
		/// 	</param>
		/// <param name="entries">- the size of the codebook.</param>
		/// <returns>the index of the entry in a codebook that best matches the input.</returns>
		public static int index(float @in, float[] codebook, int entries)
		{
			int i;
			float min_dist = 0;
			int best_index = 0;
			for (i = 0; i < entries; i++)
			{
				float dist = @in - codebook[i];
				dist = dist * dist;
				if (i == 0 || dist < min_dist)
				{
					min_dist = dist;
					best_index = i;
				}
			}
			return best_index;
		}

		/// <summary>Finds the index of the entry in a codebook that best matches the input.</summary>
		/// <remarks>Finds the index of the entry in a codebook that best matches the input.</remarks>
		/// <param name="in">- the vector to compare.</param>
		/// <param name="codebook">- the list of values to search through for the best match.
		/// 	</param>
		/// <param name="len">- the size of the vector.</param>
		/// <param name="entries">- the size of the codebook.</param>
		/// <returns>the index of the entry in a codebook that best matches the input.</returns>
		public static int index(float[] @in, float[] codebook, int len, int entries)
		{
			int i;
			int j;
			int k = 0;
			float min_dist = 0;
			int best_index = 0;
			for (i = 0; i < entries; i++)
			{
				float dist = 0;
				for (j = 0; j < len; j++)
				{
					float tmp = @in[j] - codebook[k++];
					dist += tmp * tmp;
				}
				if (i == 0 || dist < min_dist)
				{
					min_dist = dist;
					best_index = i;
				}
			}
			return best_index;
		}

		/// <summary>Finds the indices of the n-best entries in a codebook</summary>
		/// <param name="in"></param>
		/// <param name="offset"></param>
		/// <param name="codebook"></param>
		/// <param name="len"></param>
		/// <param name="entries"></param>
		/// <param name="E"></param>
		/// <param name="N"></param>
		/// <param name="nbest"></param>
		/// <param name="best_dist"></param>
		public static void nbest(float[] @in, int offset, float[] codebook, int len, int 
			entries, float[] E, int N, int[] nbest, float[] best_dist)
		{
			int i;
			int j;
			int k;
			int l = 0;
			int used = 0;
			for (i = 0; i < entries; i++)
			{
				float dist = .5f * E[i];
				for (j = 0; j < len; j++)
				{
					dist -= @in[offset + j] * codebook[l++];
				}
				if (i < N || dist < best_dist[N - 1])
				{
					for (k = N - 1; (k >= 1) && (k > used || dist < best_dist[k - 1]); k--)
					{
						best_dist[k] = best_dist[k - 1];
						nbest[k] = nbest[k - 1];
					}
					best_dist[k] = dist;
					nbest[k] = i;
					used++;
				}
			}
		}

		/// <summary>Finds the indices of the n-best entries in a codebook with sign</summary>
		/// <param name="in"></param>
		/// <param name="offset"></param>
		/// <param name="codebook"></param>
		/// <param name="len"></param>
		/// <param name="entries"></param>
		/// <param name="E"></param>
		/// <param name="N"></param>
		/// <param name="nbest"></param>
		/// <param name="best_dist"></param>
		public static void nbest_sign(float[] @in, int offset, float[] codebook, int len, 
			int entries, float[] E, int N, int[] nbest, float[] best_dist)
		{
			int i;
			int j;
			int k;
			int l = 0;
			int sign;
			int used = 0;
			for (i = 0; i < entries; i++)
			{
				float dist = 0;
				for (j = 0; j < len; j++)
				{
					dist -= @in[offset + j] * codebook[l++];
				}
				if (dist > 0)
				{
					sign = 1;
					dist = -dist;
				}
				else
				{
					sign = 0;
				}
				dist += (float)(.5 * E[i]);
				if (i < N || dist < best_dist[N - 1])
				{
					for (k = N - 1; (k >= 1) && (k > used || dist < best_dist[k - 1]); k--)
					{
						best_dist[k] = best_dist[k - 1];
						nbest[k] = nbest[k - 1];
					}
					best_dist[k] = dist;
					nbest[k] = i;
					used++;
					if (sign != 0)
					{
						nbest[k] += entries;
					}
				}
			}
		}
	}
}
