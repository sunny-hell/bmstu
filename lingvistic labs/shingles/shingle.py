# coding:utf-8
import codecs
import binascii
from Crypto.Hash import HMAC


stop_symbols = '.,!?:;-\n\r()'

stop_words = (
    u'это', u'как', u'так',
    u'и', u'в', u'над',
    u'к', u'до', u'не',
    u'на', u'но', u'за',
    u'то', u'с', u'ли',
    u'а', u'во', u'от',
    u'со', u'для', u'о',
    u'же', u'ну', u'вы',
    u'бы', u'что', u'кто',
    u'он', u'она'
)

def canonize(text):
    words = [y.strip(stop_symbols) for y in text.lower().split()]
    filtered = [x for x in words if x and (x not in stop_words)]

    return filtered

def get_hash(secret, line):
    h = HMAC.new(secret)
    h.update(line.encode('utf-8'))
    s = h.hexdigest()
    return int(s, 16)         

def gen_shingle(text, shingle_len=10):
    source = canonize(text)
    shingles = []

    for j in range(84):
        hashs_j = []
        secret = str(j) 
        for i in range(len(source) - (shingle_len - 1)):
            line = u' '.join([x for x in source[i:i + shingle_len]])
            hashs_j.append(get_hash(secret, line))
        min_hash = min(hashs_j)
        print 'index of min hash is: ', hashs_j.index(min_hash)
        shingles.append(min_hash)
         
         
    return shingles

def gen_super_shingle(text, shingle_len=10, sketch_len=4):
    shingles = gen_shingle(text, shingle_len)
    super_shingles = []
    for i in range(0, len(shingles), sketch_len):
        ss_line = ' '.join([str(x) for x in shingles[i:i+sketch_len]])
        super_shingles.append(binascii.crc32(ss_line))
    return super_shingles            

def gen_mega_shingle(text, shingle_len=10, sketch_len=4):
    super_shingles =  gen_super_shingle(text, shingle_len, sketch_len)
    mega_shingles = []
    for x in super_shingles:
        for y in super_shingles:
            if x != y:
                mega_line = ' '.join([str(x), str(y)])
                mega_shingles.append(binascii.crc32(mega_line))
    return mega_shingles

def compare(source1, source2):
    same = 0
    for i in range(len(source1)):
        if source1[i] == source2[i]:
            same = same + 1
    return same

def compare_simple(source1, source2):
    return compare(source1, source2) / 84.0 * 100

def read_file(filename):
    with codecs.open(filename, encoding='utf-8') as fin:
        return fin.read()


if __name__ == '__main__':
    import argparse
    parser = argparse.ArgumentParser()
    parser.add_argument('filename1')
    parser.add_argument('filename2')
    parser.add_argument('--len', help='shingle length', type=int, default=5,
                        action='store')
    parser.add_argument('--alg', help='shingle algorithm (simple, super, mega)', type=str, default='simple',
                        action='store')
    parser.add_argument('--sketch', help='sketch length for super shingle', type=int, default=4,
                        action='store')
    args = parser.parse_args()

    text1 = read_file(args.filename1)
    text2 = read_file(args.filename2)
    
    if args.alg == 'simple':
        cmp1 = gen_shingle(text1, shingle_len=args.len)
        cmp2 = gen_shingle(text2, shingle_len=args.len)
        print 'resemblance is {}%'.format(compare_simple(cmp1, cmp2))
    elif args.alg == 'super':
        cmp1 = gen_super_shingle(text1, shingle_len=args.len, sketch_len=args.sketch)
        cmp2 = gen_super_shingle(text2, shingle_len=args.len, sketch_len=args.sketch)
        print 'number of matched super shingles is {}'.format(compare(cmp1, cmp2))    
    elif args.alg == 'mega':
        cmp1 = gen_mega_shingle(text1, shingle_len=args.len, sketch_len=args.sketch)
        cmp2 = gen_mega_shingle(text2, shingle_len=args.len, sketch_len=args.sketch)
        print 'number of matched mega shingles is {}'.format(compare(cmp1, cmp2))  
