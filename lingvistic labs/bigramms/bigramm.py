# coding:utf-8
import codecs
import binascii
import nltk
from pymorphy import get_morph

def read_file(filename):
    with codecs.open(filename, encoding='utf-8') as fin:
        return fin.read()

def get_pairs(words, wlen):
    pairs = []
    for i in range(0, len(words)):
        for j in range(i+1, min(i+wlen, len(words))):
            pairs.append(((words[i], words[j]),j-i))
    return pairs
    

def process_text(text, w_len, verbose):
    sent_detector = nltk.data.load('tokenizers/punkt/english.pickle')
    sentences = sent_detector.tokenize(text.strip())
    bigramms = []
    
    for s in sentences:
        words=nltk.PunktWordTokenizer().tokenize(s)
        words = [w.strip('.,- ;:!?()') for w in words]
        words = filter(lambda x: len(x) >= 3 and x not in nltk.stem.snowball.stopwords.words('english'), words)
        bigramms.extend(get_pairs(words, w_len))
    print ' total: ', len(bigramms)   
    N = len(bigramms)
    bigr_map = {}
    i = 0
    bigr_set = set(bigramms) 
    #for b in bigramms:
    #    if b[0] not in bigr_map.keys():
    #        bigr_map[b[0]] = [0,0,0]
    #    bigr_map[b[0]][0] +=1
    #    bigr_map[b[0]][1] += b[1]
    #    i+=1
    #    print 'creating bigramm map... {}%'.format(i/float(N)*100)
    # get standart dev
    #for b in bigramms:
    #    bigr_map[b[0]][2] += (b[1] - bigr_map[b[0]][1] / bigr_map[b[0]][0])**2
    total = len(bigr_set)
    count = 0
    collocations = []
    for b in bigr_set:
        #if verbose:
        #    print '#######################'
        #    print key
        #    print 'count=', value[0]
        #if value[0] <= 1:
        #    continue
        #value[2] /= value[0]-1
        #if verbose:
        #    print u'dispersia: ', value[2]  
        #if value[2] <= 1.5:
        matches = len(filter(lambda p: p[0] == b[0], bigramms))
        first_matches = len(filter(lambda p: p[0][0] == b[0][0], bigramms))-matches
        second_matches = len(filter(lambda p: p[0][1] == b[0][1], bigramms))-matches 
        non_matches = N - matches - first_matches - second_matches
        if verbose:
            print 'matches: ', matches
            print 'first_matches: ', first_matches
            print 'second_matches: ', second_matches
            print 'non_matches: ', non_matches
        try:
            hi_numer = N*((matches*non_matches - first_matches*second_matches)**2)
            hi_denom = (matches + first_matches)*(matches + second_matches)*(non_matches + first_matches)*(non_matches + second_matches)
            hi = hi_numer / hi_denom
            if verbose:
                print 'hi is: ', hi
            if hi < 7.88:
                if verbose:
               # print '#######################'
               # print key
                    print 'collocation'
                    collocations.appned(b)
            elif verbose :
                print 'not collocation'
        except Exception as e:
            print repr(e)
            print 'error in pair ', b
        #elif verbose:
        #    print 'not collocation'
        count+=1;
        print 'analysed: {}%'.format(count/float(total)*100)
    if not verbose:
        print 'collocations are: ', collocations    
if __name__ == '__main__':
    import argparse
    parser = argparse.ArgumentParser()
    parser.add_argument('filename')
    parser.add_argument('--w', help='window length', type=int, default=5,
                        action='store')
    parser.add_argument('--v', help='verbose', action='store_true')
    
    args = parser.parse_args()
    text = read_file(args.filename)
    w_len = args.w
    verbose = args.v
    #words = [w for w in words if not w in
    
    #morph = get_morph('/home/sunny-hell/morphy/ru.sqlite')
    #info = morph.get_graminfo(u"КОМПЬЮТЕР")
   # print info[0]['norm'] # нормальная форма

    #print info[0]['class'] # часть речи, С = существительное

    #print info[0]['info'] # род, число, падеж и т.д.

    process_text(text, w_len, verbose)
    
           
