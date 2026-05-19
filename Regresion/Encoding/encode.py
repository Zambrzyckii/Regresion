import pandas as pd
import sys

input_file = sys.argv[1]
output_file = sys.argv[2]

df = pd.read_csv(input_file)

encoded_df = pd.get_dummies(df)

encoded_df.to_csv(output_file,index = False)
